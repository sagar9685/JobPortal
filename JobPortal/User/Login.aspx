<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JobPortal.User.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Center the form on the page */
        .form-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 20px;
            background-color: #f2f2f2;
        }

        .button-contactForm {
            margin: 0 auto; /* Center the button horizontally */
        }


        .contact_form {
            max-width: 500px;
            width: 100%;
            background-color: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
        }

        /* Form Title */
        .contact-title {
            text-align: center;
            margin-bottom: 30px;
            color: #333;
            font-size: 24px;
            font-weight: 600;
        }

        /* Form Inputs */
        .form-group {
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
        }

        /* Submit Button */
        .button-contactForm {
            width: 100%;
            padding: 12px;
            background-color: #5cb85c;
            color: #fff;
            border: none;
            border-radius: 5px;
            font-size: 18px;
            font-weight: bold;
            cursor: pointer;
        }

            .button-contactForm:hover {
                background-color: #4cae4c;
            }

        /* Links */
        .clickLink {
            display: block;
            text-align: center;
            margin-top: 15px;
        }

            .clickLink a {
                color: #007bff;
                text-decoration: none;
            }

                .clickLink a:hover {
                    text-decoration: underline;
                }

        /* Alert Message */
        .msg-box {
            text-align: center;
            margin-bottom: 20px;
        }

        .alert {
            padding: 10px;
            border-radius: 5px;
            text-align: center;
        }

        .alert-danger {
            color: #721c24;
            background-color: #f8d7da;
            border-color: #f5c6cb;
        }

        .alert-success {
            color: #155724;
            background-color: #d4edda;
            border-color: #c3e6cb;
        }
    </style>
    <%-- <script src="https://www.google.com/recaptcha/api.js" async defer></script> --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="form-container">
        <div class="form-contact contact_form">
            <div class="row">
                <!-- Page Title -->
                <div class="col-12 pb-20">
                    <h2 class="contact-title">Sign In</h2>
                </div>

                <!-- Success/Error Message -->
                <div class="col-12 msg-box">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>

                <!-- Username Input -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Username</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter Username" required></asp:TextBox>
                    </div>
                </div>

                <!-- Password Input -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Password</label>
                        <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password" required></asp:TextBox>
                    </div>
                </div>

                <!-- Login Type Dropdown -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Login Type</label>
                        <asp:DropDownList ID="ddlLoginType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Select Login Type</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                            <asp:ListItem>User</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Login Type is required" ForeColor="Red" Display="Dynamic"
                            SetFocusOnError="true" Font-Size="Small" InitialValue="0"
                            ControlToValidate="ddlLoginType">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <!-- Google reCAPTCHA Widget -->
                <!-- Include reCAPTCHA script in the head section -->
<!-- reCAPTCHA widget in the form -->
<div class="form-group">
    <div class="g-recaptcha" data-sitekey="6Lc58mMqAAAAAK5oovgG7ozwxBvpcR25F3XtjabY"></div> <!-- Replace with your actual site key -->
    <asp:CustomValidator ID="recaptchaValidator" runat="server" ErrorMessage="Please verify you are not a robot." CssClass="text-danger" OnServerValidate="ValidateCaptcha"></asp:CustomValidator>
</div>

<script src="https://www.google.com/recaptcha/api.js" async defer></script>


 <!-- Replace with your actual Site Key -->


                <div class="form-group mt-3" style="text-align: center;">
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="button-contactForm boxed-btn" OnClick="btnLogin_Click" Style="display: inline-block;" />
                    <span class="clickLink" style="display: block; margin-top: 10px;">
                        <a href="../User/Register.aspx">New User? Click Here...</a>
                    </span>
                    <span class="clickLink" style="display: block; margin-top: 10px;">
                        <a href="../User/ChangePassword.aspx">Change Password</a>
                    </span>
                </div>



            </div>
        </div>
    </section>
</asp:Content>
