# Overview Project
This is a basic e-commerce project, the application has some of the basic features of an e-commerce site. The application is divided into three basic parts including:
## 1. Backend (built with ASP.NET API and Identity Server 4 and EntityFrameWork Core):
- Querying data and handling basic tasks of a server.
- Provide data for other websites to use.
- Integrate identity server to authorize and authenticate users for the purpose of ensuring server security.
- Use Cookies and JWTs for authentication and authority.
## 2. Customer Site (built with ASP.NET MVC and Razor page):
- Show list of shop's sold products and pagination.
- View product details.
- Product classification by product type.
- To make a purchase, the user must log in (log in incorrectly 5 times to lock the account).
- After making a purchase, the user can rate the purchased product.
## 3. Admin Site (built with ReactJS and Redux): Only users with admin rights can log in and perform operations on the page. The functions on the admin page:
- Log in and log out.
- Lock or unlock customer accounts.
- Paging user list.
- Manage product categories (CRUD Category).
- Manage products (CRUD Product).
- View list of orders, view invoice details, and confirm orders.
## 4. Unit Test (built with XunitTest):
- The general purpose is to test if the server functions are working correctly. Here, only test the controllers.