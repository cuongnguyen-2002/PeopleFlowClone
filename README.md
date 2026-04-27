1. Bạn tổ chức code theo cách nào? Vì sao chọn cách tổ chức đó?
+ Component-based + Data-driven + Event-driven dùng cách này sẽ đảm bảo tách biệt các thành phần ra với nhau, mỗi class sẽ có trách nhiệm riêng của nó 
+ Đặc biệt không để GameManager ôm tất cả logic của game tránh tình trạng GameManager sẽ trở nên quá lớn khó mở rộng về sau
+ Dễ thay đổi từng phần mà không ảnh hưởng tới phần khác
2. Nếu cần làm **100 level**, bạn sẽ mở rộng hệ thống hiện tại như thế nào?
+ Nếu làm 100 level hệ thống sẽ mở ra theo hướng dữ liệu của level sẽ được lưu dướng dạng data asset thông qua ScriptableObject sau đó sẽ load level dựa trên config để không phải tạo từng scene nếu như thêm một level mới
3. Nếu dữ liệu cho thấy người chơi **fail quá nhiều**, bạn sẽ điều chỉnh gameplay ở đâu trước?
+ Nếu dữ liệu cho thấy người chơi fail quá nhiều thì gameplay nên điều chỉnh ở level data trước ví dụ như tăng thời gian màn chơi, sắp xếp lại thứ tự của gate/hole giảm số lượng lane hoặc số minion trong hàng chờ tăng thời gian khoảng cách giữa các lần tab
+ cho phù hợp chứ không nên sửa thẳng vào core gameplay
4. Theo bạn, **mechanic nào trong prototype này ảnh hưởng retention nhiều nhất?** Vì sao?
+ Trong prototye này ảnh hưởng nhất chính là tab để đưa minion lên băng truyền bởi vì nó tạo cảm giác tương tác liên tục, sự cân nhắc khi băng truyền gần đầy, cảm giác thất bại đôi chút khi spam quá nhanh để đầy băng truyền dẫn đến thất bãi
nhưng không quá khó để thử lại trong lần tiếp theo do cơ chế đơn giản nhìn vào hiểu ngay mà game đem lại.

***LINK DEMO:
https://drive.google.com/file/d/1fd7rCYJuf_1ztvIoSDCSQQGARRtue0kx/view?usp=sharing
