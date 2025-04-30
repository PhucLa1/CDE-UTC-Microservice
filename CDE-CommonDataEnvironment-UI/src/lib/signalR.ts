import { HubConnection, HubConnectionBuilder, LogLevel, HttpTransportType } from "@microsoft/signalr";

// Định nghĩa interface cho message nhận từ server
interface AnnotationMessage {
    viewId: number;
    inkString: string;
    annotationAction: number;
}

class SignalRService {
    private connection: HubConnection | null = null;

    // Khởi tạo kết nối tới hub
    public async startConnection(url: string): Promise<void> {
        if (this.connection && this.connection.state === "Connected") {
            console.log("SignalR is already connected");
            return;
        }

        this.connection = new HubConnectionBuilder()
            .withUrl(url, {
                transport: HttpTransportType.WebSockets, // Chỉ định WebSocket để tối ưu
            })
            .configureLogging(LogLevel.Debug) // Log chi tiết hơn để debug
            .withAutomaticReconnect() // Tự động kết nối lại nếu mất kết nối
            .build();

        try {
            await this.connection.start();
            console.log("SignalR Connected");
        } catch (err) {
            console.error("SignalR Connection Error123: ", err);
            throw err; // Ném lỗi để xử lý ở cấp cao hơn
        }
    }

    // Tham gia channel
    public async joinChannel(viewId: number): Promise<void> {
        await this.ensureConnected();
        try {
            await this.connection!.invoke("JoinChannel", viewId);
            console.log(`Joined channel ${viewId}`);
        } catch (err) {
            console.error("Error joining channel: ", err);
            throw err;
        }
    }

    // Rời channel
    public async leaveChannel(viewId: number): Promise<void> {
        await this.ensureConnected();
        try {
            await this.connection!.invoke("LeaveChannel", viewId);
            console.log(`Left channel ${viewId}`);
        } catch (err) {
            console.error("Error leaving channel: ", err);
            throw err;
        }
    }

    // Gửi annotation tới channel
    public async sendAnnotation(viewId: number, annotation: string, action: number): Promise<void> {
        await this.ensureConnected();
        try {
            await this.connection!.invoke("SendAnnotationToChannel", viewId, annotation, action);
            console.log(`Sent annotation to channel ${viewId}`);
        } catch (err) {
            console.error("Error sending annotation: ", err);
            throw err;
        }
    }

    // Lắng nghe message từ server
    public onReceiveAnnotation(callback: (message: AnnotationMessage) => void): void {
        if (this.connection) {
            this.connection.on("ReceiveAnnotation", (viewId: number, inkString: string, annotationAction: number) => {
                const message: AnnotationMessage = { viewId, inkString, annotationAction };
                callback(message);
            });
        } else {
            console.warn("Cannot set listener: SignalR connection is not initialized");
        }
    }

    // Ngắt kết nối
    public async stopConnection(): Promise<void> {
        if (this.connection) {
            try {
                await this.connection.stop();
                console.log("SignalR Disconnected");
            } catch (err) {
                console.error("Error stopping SignalR: ", err);
            }
            this.connection = null; // Đặt lại connection để tránh tái sử dụng
        }
    }

    // Helper để đảm bảo kết nối
    private async ensureConnected(): Promise<void> {
        if (!this.connection) {
            throw new Error("SignalR connection is not initialized");
        }
        if (this.connection.state !== "Connected") {
            throw new Error(`SignalR connection is in '${this.connection.state}' state, expected 'Connected'`);
        }
    }
}

export default new SignalRService();