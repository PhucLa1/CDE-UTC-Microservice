
import { useEffect, useRef } from 'react';

const PDFViewer = ({ url }: { url: string }) => {
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
                // Có thể thêm các cấu hình hoặc API calls ở đây
                console.log('PDFTron WebViewer đã sẵn sàng!');
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