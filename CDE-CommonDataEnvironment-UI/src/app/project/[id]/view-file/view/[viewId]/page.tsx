"use client";
import React, { useEffect, useState } from 'react';
import PDFViewer from '../../_components/pdf-viewer';
import { Annotation } from '@/data/schema/Project/view.schema';
import signalRService from "../../../../../../lib/signalR";
import { useQuery } from '@tanstack/react-query';
import viewApiRequest from '@/apis/view.api';

export default function ViewerPageView({ params }: { params: { viewId: string } }) {
    const [fileUrl, setFileUrl] = useState<string | null>(null);
    const [annotationList, setAnnotationList] = useState<Annotation[]>([]);
    const { data, isLoading } = useQuery({
        queryKey: ['get-annotation'],
        queryFn: () => viewApiRequest.getAnnotationByViewId(Number(params.viewId)),
    })
    useEffect(() => {
        if (data) setAnnotationList(data.data);
    }, [data])
    useEffect(() => {
        const setupSignalR = async () => {
            try {
                await signalRService.startConnection("https://localhost:5052/annotation-hub");
                await signalRService.joinChannel(Number(params.viewId));
                signalRService.onReceiveAnnotation((message) => {
                    setAnnotationList((prev) => [...prev, message]);
                    console.log("On Receive Message : ", message)
                });
            } catch (err) {
                console.error("SignalR Setup Error: ", err);
            }
        };

        setupSignalR();
    }, [params.viewId]);
    const addAnnotation = (annotation: Annotation) => {
        console.log("Annotation added: ", annotation)
    };


    useEffect(() => {
        // Lấy URL từ query parameter trên client-side
        const currentUrl = window.location.href;
        const urlParams = new URL(currentUrl).searchParams;
        const url = urlParams.get('url');

        if (url) {
            setFileUrl(decodeURIComponent(url)); // Giải mã URL để lấy giá trị gốc
        }
    }, []); // Chỉ chạy một lần khi component mount


    if (!fileUrl || isLoading) {
        return <div>Đang tải...</div>; // Hiển thị loading trong khi lấy URL
    }

    return (
        <div>
            {annotationList.length != 0 && <PDFViewer viewId={Number(params.viewId)} annotationList={annotationList} addAnnotation={addAnnotation} url={fileUrl} />}
        </div>
    );
}