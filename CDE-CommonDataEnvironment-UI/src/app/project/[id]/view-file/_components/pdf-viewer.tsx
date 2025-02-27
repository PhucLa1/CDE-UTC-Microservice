
import { AnnotationAction } from '@/data/enums/annotationaction.enum';
import { annotationSchema } from '@/data/schema/Project/view.schema';
import { useEffect, useRef, useState } from 'react';
import { z } from 'zod';

type FormProps = {
    url: string,
    addAnnotation: (value: z.infer<typeof annotationSchema>) => void
}

const PDFViewer = ({ url, addAnnotation }: FormProps) => {
    const viewer = useRef<any>(null);

    useEffect(() => {
        import('@pdftron/webviewer').then((WebViewer) => {
            WebViewer.default(
                {
                    path: '/webviewer', // Đường dẫn tới thư mục tĩnh trong public
                    initialDoc: url, // URL của file PDF,
                },
                viewer.current
            ).then((instance) => {
                const { documentViewer, annotationManager } = instance.Core;

                const xfdfString = ``;


                documentViewer.addEventListener('documentLoaded', async () => {
                    const annotations = await annotationManager.importAnnotationCommand(xfdfString);
                    annotations.forEach(annotation => {
                        console.log(annotation)
                        annotationManager.redrawAnnotation(annotation);
                    });
                })

                annotationManager.addEventListener('annotationChanged', async (annotations, action, { imported }) => {
                    // If the event is triggered by importing then it can be ignored
                    // This will happen when importing the initial annotations
                    // from the server or individual changes from other users
                    if (imported) return;

                    const xfdfString = await annotationManager.exportAnnotationCommand();

                    // Parse chuỗi XFDF thành DOM
                    const parser = new DOMParser();
                    const xmlDoc = parser.parseFromString(xfdfString, 'text/xml');
                    console.log(action)
                    switch (action) {
                        case 'add':
                            const addElement = xmlDoc.getElementsByTagName('add')[0]?.children;
                            if (addElement && addElement.length > 0) {
                                const singleAddElement = addElement[0];
                                const addContent = new XMLSerializer().serializeToString(singleAddElement);
                                addAnnotation({
                                    inkString: addContent,
                                    annotationAction: AnnotationAction.ADD,
                                    viewId: 0
                                })
                            }
                            break;
                        case 'delete':
                            const deleteElement = xmlDoc.getElementsByTagName('delete')[0]?.children;
                            if (deleteElement && deleteElement.length > 0) {
                                const deleteContent = new XMLSerializer().serializeToString(deleteElement[0]);
                                addAnnotation({
                                    inkString: deleteContent,
                                    annotationAction: AnnotationAction.DELETE,
                                    viewId: 0
                                })
                            }
                            break;
                        case 'modify':
                            const modifyElement = xmlDoc.getElementsByTagName('modify')[0]?.children;
                            if (modifyElement && modifyElement.length > 0) {
                                const modifyContent = new XMLSerializer().serializeToString(modifyElement[0]);
                                addAnnotation({
                                    inkString: modifyContent,
                                    annotationAction: AnnotationAction.UPDATE,
                                    viewId: 0
                                })
                            }
                            break;
                        default:
                            console.log('Hành động không xác định:', action, xfdfString);
                    }
                    // <xfdf>
                    //   <add>
                    //     <text subject="Comment" page="0" color="#FFE6A2" ... />
                    //   </add>
                    //   <modify />
                    //   <delete />
                    // </xfdf>
                    console.log(xfdfString);
                });
            });
        });
    }, []);

    return (
        <div
            ref={viewer}
            style={{ height: '100vh', width: '100%' }}>
        </div>
    );
};

export default PDFViewer;