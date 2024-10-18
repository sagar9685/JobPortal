<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="JobPortal.User.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* You can add styles similar to the login page or customize them for this page */
        .form-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 20px;
            background-color: #f2f2f2;
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="form-container">
        <div class="form-contact contact_form">
            <div class="row">
                <div class="col-12 pb-20">
                    <h2 class="contact-title">Change Password</h2>
                </div>

                <div class="col-12 msg-box">
                    <asp:Label ID="lblChangePasswordMsg" runat="server" Visible="false"></asp:Label>
                </div>

                <!-- Username Input -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username" required></asp:TextBox>
                    </div>
                </div>

                <!-- Current Password Input -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Current Password</label>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Current Password" required></asp:TextBox>
                    </div>
                </div>

                <!-- New Password Input -->
                <div class="col-12">
                    <div class="form-group">
                        <label>New Password</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter New Password" required></asp:TextBox>
                    </div>
                </div>

                <!-- Confirm New Password Input -->
                <div class="col-12">
                    <div class="form-group">
                        <label>Confirm New Password</label>
                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm New Password" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group mt-3" style="text-align: center;">
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="button-contactForm boxed-btn" OnClick="btnChangePassword_Click" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
