<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="JobPortal.User.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Centering the form and giving a modern look */
        .form-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 20px;
            background: linear-gradient(135deg, #74ebd5, #acb6e5); /* Background gradient */
        }

        .contact_form {
            max-width: 600px;
            width: 100%;
            background-color: #ffffff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1); /* Softer shadow */
        }

        .contact-title {
            text-align: center;
            font-size: 28px;
            margin-bottom: 30px;
            color: #333;
            font-weight: bold;
        }

        /* Stylish form elements */
        .form-group {
            margin-bottom: 25px;
            position: relative;
        }

        label {
            font-weight: bold;
            color: #555;
            display: block;
            margin-bottom: 8px;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 16px;
            transition: border-color 0.3s ease;
        }

        .form-control:focus {
            border-color: #4caf50; /* Green border on focus */
        }

        .button-contactForm {
            width: 100%;
            padding: 14px;
            background-color: #4caf50; /* Green button */
            color: #fff;
            border: none;
            border-radius: 6px;
            font-size: 18px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .button-contactForm:hover {
            background-color: #45a049; /* Darker green on hover */
        }

        /* Alert boxes for success and error messages */
        .alert {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 6px;
            text-align: center;
        }

        .alert-success {
            color: #155724;
            background-color: #d4edda;
            border-color: #c3e6cb;
        }

        .alert-danger {
            color: #721c24;
            background-color: #f8d7da;
            border-color: #f5c6cb;
        }

        /* Link below the button */
        .clickLink {
            display: block;
            text-align: center;
            margin-top: 20px;
        }

        .clickLink a {
            color: #4caf50;
            text-decoration: none;
            font-weight: bold;
        }

        .clickLink a:hover {
            text-decoration: underline;
        }

        /* Responsive layout for smaller devices */
        @media (max-width: 576px) {
            .contact_form {
                padding: 20px;
            }

            .contact-title {
                font-size: 24px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="form-container">
        <div class="form-contact contact_form">
            <div class="row">
                <div class="col-12 pb-20">
                    <h2 class="contact-title">Sign Up</h2>
                </div>

                <!-- Success/Error Message -->
                <div class="col-12 msg-box">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>

                <!-- Login Information -->
                <div class="col-12">
                    <h6>Login Information</h6>
                </div>
                <div class="col-12">
                    <div class="form-group">
                        <label>Username</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter Username" required></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label>Password</label>
                        <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password" required></asp:TextBox>
                        <asp:RegularExpressionValidator ID="PasswordValidator" runat="server"
                            ControlToValidate="TxtPassword"
                            ErrorMessage="Password must be Long"
                            CssClass="text-danger"
                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label>Confirm Password</label>
                        <asp:TextBox ID="TxtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Confirm Password" required></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtConfirmPassword" ControlToCompare="TxtPassword" ErrorMessage="Passwords do not match." ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" />
                    </div>
                </div>

                <!-- Personal Information -->
                <div class="col-12 mt-2">
                    <h6>Personal Information</h6>
                </div>
                <div class="col-12">
                    <div class="form-group">
                        <label>Full Name</label>
                        <asp:TextBox ID="TxtFullName" runat="server" CssClass="form-control" placeholder="Enter Full Name" required></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtFullName" ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="Name must be alphabetical." ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <!-- Address -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Address</label>
                        <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" required></asp:TextBox>
                    </div>
                </div>

                <!-- Mobile No -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Mobile No</label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile No" required></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobile" ValidationExpression="^[6-9]\d{9}$" ErrorMessage="Mobile No must be 10 digits and start with 6-9." ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" />
                    </div>
                </div>

                <!-- Email -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Email</label>
                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" required TextMode="Email"></asp:TextBox>
                    </div>
                </div>

                <!-- Country Dropdown -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control" AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName">
                            <asp:ListItem Value="0">Select Country</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Country is required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CountryName] FROM [Country]"></asp:SqlDataSource>
                    </div>
                </div>

                <!-- Submit Button -->
                <div class="form-group mt-3">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button button-contactForm boxed-btn" OnClick="btnRegister_Click" />
                    <span class="clickLink"><a href="../User/Login.aspx">Already Registered? Click Here...</a></span>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
