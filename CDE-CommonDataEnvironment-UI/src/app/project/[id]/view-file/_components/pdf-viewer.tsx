import viewApiRequest from '@/apis/view.api';
import { AnnotationAction } from '@/data/enums/annotationaction.enum';
import { Annotation, annotationSchema } from '@/data/schema/Project/view.schema';
import { handleSuccessApi } from '@/lib/utils';
import { useMutation } from '@tanstack/react-query';
import { useEffect, useRef } from 'react';
import { z } from 'zod';

type FormProps = {
    url: string,
    addAnnotation: (value: z.infer<typeof annotationSchema>) => void,
    annotationList: Annotation[],
    viewId: number
}

const PDFViewer = ({ url, addAnnotation, annotationList, viewId }: FormProps) => {
    const viewer = useRef<any>(null);
    const instanceRef = useRef<any>(null);

    const { mutate } = useMutation({
        mutationKey: ['change-annotation'],
        mutationFn: (value: Annotation) => viewApiRequest.createAnnotation(value),
        onSuccess: () => {
            handleSuccessApi({ title: 'Cập nhật thành công' });
        }
    });

    const annotationString = (): string => {
        let addString = "";
        let modifyString = "";
        let deleteString = "";
        annotationList.forEach((item) => {
            if (item.annotationAction === AnnotationAction.ADD) addString += item.inkString;
            else if (item.annotationAction === AnnotationAction.UPDATE) modifyString += item.inkString;
            else if (item.annotationAction === AnnotationAction.DELETE) deleteString += item.inkString;
        });
        const xfdfString = `<?xml version="1.0" encoding="UTF-8" ?>
                <xfdf xmlns="http://ns.adobe.com/xfdf/" xml:space="preserve">
                    <fields />
                    <add>${addString}</add>
                    <modify>${modifyString}</modify>
                    <delete>${deleteString}</delete>
                </xfdf>`;
        console.log("Generated XFDF String:", xfdfString);
        return xfdfString;
    };

    // Khởi tạo WebViewer
    useEffect(() => {
        import('@pdftron/webviewer').then((WebViewer) => {
            WebViewer.default(
                {
                    path: '/webviewer',
                    initialDoc: url,
                    licenseKey: 'demo:phucminhbeos@gmail.com:6172c05b0200000000b77d00b444f3fbcad58a45390b23bf26eeb11e1e'
                },
                viewer.current
            ).then((instance) => {
                instanceRef.current = instance;
                const { documentViewer, annotationManager } = instance.Core;

                documentViewer.addEventListener('documentLoaded', async () => {
                    const xfdfString = annotationString();
                    console.log("Initial XFDF on document load:", xfdfString);
                    const annotations = await annotationManager.importAnnotationCommand(xfdfString);
                    annotationManager.drawAnnotationsFromList(annotations);
                });

                annotationManager.addEventListener('annotationChanged', async (annotations, action, { imported }) => {
                    console.log(annotations)
                    if (imported) return;

                    const xfdfString = await annotationManager.exportAnnotationCommand();
                    const parser = new DOMParser();
                    const xmlDoc = parser.parseFromString(xfdfString, 'text/xml');

                    switch (action) {
                        case 'add':
                            const addElement = xmlDoc.getElementsByTagName('add')[0]?.children;
                            console.log(addElement)
                            if (addElement?.length > 0) {
                                const addContent = new XMLSerializer().serializeToString(addElement[0]);
                                addAnnotation({ inkString: addContent, annotationAction: AnnotationAction.ADD, viewId });
                                if (viewId !== 0) mutate({ inkString: addContent, action: AnnotationAction.ADD, viewId });
                            }
                            break;
                        case 'delete':
                            const deleteElement = xmlDoc.getElementsByTagName('delete')[0]?.children;
                            if (deleteElement?.length > 0) {
                                const deleteContent = new XMLSerializer().serializeToString(deleteElement[0]);
                                addAnnotation({ inkString: deleteContent, annotationAction: AnnotationAction.DELETE, viewId });
                                if (viewId !== 0) mutate({ inkString: deleteContent, action: AnnotationAction.DELETE, viewId });
                            }
                            console.log('Delete action detected:', deleteElement);
                            break;
                        case 'modify':
                            const modifyElement = xmlDoc.getElementsByTagName('modify')[0]?.children;
                            if (modifyElement?.length > 0) {
                                const modifyContent = new XMLSerializer().serializeToString(modifyElement[0]);
                                addAnnotation({ inkString: modifyContent, annotationAction: AnnotationAction.UPDATE, viewId });
                                if (viewId !== 0) mutate({ inkString: modifyContent, action: AnnotationAction.UPDATE, viewId });
                            }
                            break;
                        default:
                            console.log('Unknown action:', action, xfdfString);
                    }
                });
            });
        });
    }, [url]);

    // Cập nhật annotation khi annotationList thay đổi
    useEffect(() => {
        if (!instanceRef.current) {
            console.log("WebViewer not initialized yet.");
            return;
        }

        const { annotationManager, documentViewer } = instanceRef.current.Core;
        if (!documentViewer.getDocument()) {
            console.log("Document not loaded yet.");
            return;
        }

        const xfdfString = annotationString();
        console.log("Updating annotations with XFDF:", xfdfString);

        const reDraw = async () => {
            console.log(annotationManager, documentViewer)
            const xfdfString = annotationString();
            console.log("Initial XFDF on document load:", xfdfString);
            const annotations = await annotationManager.importAnnotationCommand(xfdfString);
            annotationManager.drawAnnotationsFromList(annotations);
        }

        reDraw()
    }, [annotationList]);

    return (
        <div ref={viewer} style={{ height: '100vh', width: '100%' }} />
    );
};

export default PDFViewer;