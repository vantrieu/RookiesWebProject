#Overview Project
This is a basic e-commerce project, the application has some of the basic features of an e-commerce site. The application is divided into three basic parts including:
##1. Backend (built with ASP.NET API and Identity Server 4 and EntityFrameWork Core):
1.1. Querying data and handling basic tasks of a server.
1.2. Provide data for other websites to use.
1.3. Integrate identity server to authorize and authenticate users for the purpose of ensuring server security.
1.4. Use Cookies and JWTs for authentication and authority.
##2. Customer Site (built with ASP.NET MVC and Razor page):
2.1. Show list of shop's sold products and pagination.
2.2. View product details.
2.3. Product classification by product type.
2.4. To make a purchase, the user must log in (log in incorrectly 5 times to lock the account).
2.5. After making a purchase, the user can rate the purchased product.
##3. Admin Site (built with ReactJS and Redux): Only users with admin rights can log in and perform operations on the page. The functions on the admin page:
3.1. Log in and log out.
3.2. Lock or unlock customer accounts.
3.3. Paging user list.
3.4. Manage product categories (CRUD Category).
3.5. Manage products (CRUD Product).
3.6. View list of orders, view invoice details, and confirm orders.
##4. Unit Test (built with XunitTest):
- The general purpose is to test if the server functions are working correctly. Here, only test the controllers.