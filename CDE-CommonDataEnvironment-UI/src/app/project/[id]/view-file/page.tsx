"use client";
import React, { useEffect, useState } from 'react';
import PDFViewer from './_components/pdf-viewer';

export default function ViewerPage() {
  const [fileUrl, setFileUrl] = useState<string | null>(null);

  useEffect(() => {
    // Lấy URL từ query parameter trên client-side
    const currentUrl = window.location.href;
    const urlParams = new URL(currentUrl).searchParams;
    const url = urlParams.get('url');
    
    if (url) {
      setFileUrl(decodeURIComponent(url)); // Giải mã URL để lấy giá trị gốc
    }
  }, []); // Chỉ chạy một lần khi component mount
  

  if (!fileUrl) {
    return <div>Đang tải...</div>; // Hiển thị loading trong khi lấy URL
  }

  return (
    <div>
      <h1>Xem file với PDFTron trong Next.js</h1>
      <PDFViewer url={fileUrl} />
    </div>
  );
}