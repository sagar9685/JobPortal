<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="ResumeBuild.aspx.cs" Inherits="JobPortal.User.ResumeBuild" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Global Reset */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        /* Styling the body */
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f9; /* Light gray background */
            color: #333;
            line-height: 1.6;
        }

        /* Container for the entire form */
        .form-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 40px;
            background-color: #f4f4f9; /* Neutral background */
        }

        /* Contact form styles */
        .contact_form {
            background-color: #fff;
            padding: 40px;
            border-radius: 15px;
            max-width: 800px;
            width: 100%;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            transition: all 0.3s ease-in-out;
        }

        .contact_form:hover {
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.2);
        }

        /* Form Header */
        .contact_form h2 {
            font-size: 26px;
            text-align: center;
            margin-bottom: 20px;
            color: #333;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        /* Form group layout */
        .form-group {
            margin-bottom: 25px;
        }

        /* Label styling */
        .form-group label {
            display: block;
            font-size: 14px;
            color: #666;
            font-weight: bold;
            margin-bottom: 10px;
        }

        /* Textbox styling */
        .form-control {
            width: 100%;
            padding: 12px;
            font-size: 15px;
            border: 2px solid #ddd;
            border-radius: 8px;
            transition: all 0.3s ease;
        }

        .form-control:focus {
            border-color: #2575fc;
            box-shadow: 0 0 8px rgba(37, 117, 252, 0.4);
        }

        /* Styling the button */
        .button-contactForm {
            display: inline-block;
            padding: 12px 25px;
            background-color: #2575fc;
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: bold;
            text-transform: uppercase;
            cursor: pointer;
            width: 100%;
            transition: background-color 0.3s ease;
        }

        .button-contactForm:hover {
            background-color: #155ec9;
        }

        /* Success/Error Message */
        .msg-box {
            margin-bottom: 20px;
            text-align: center;
        }

        /* Error and success message style */
        .alert {
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 5px;
            text-align: center;
        }

        .alert-success {
            background-color: #dff0d8;
            color: #3c763d;
        }

        .alert-danger {
            background-color: #f2dede;
            color: #a94442;
        }

        /* Responsive for smaller screens */
        @media (max-width: 768px) {
            .contact_form {
                padding: 20px;
            }
        }

        @media (max-width: 576px) {
            .form-control {
                font-size: 14px;
                padding: 10px;
            }

            .button-contactForm {
                font-size: 14px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="form-container">
        <div class="form-contact contact_form">
            <h2>Build Your Resume</h2>

            <!-- Success/Error Message -->
            <div class="msg-box">
                <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert"></asp:Label>
            </div>

            <!-- Personal Information -->
            <div class="form-group">
                <label>Full Name</label>
                <asp:TextBox ID="TxtFullName" runat="server" CssClass="form-control" placeholder="Enter Full Name" required></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtFullName" ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="Name must be alphabetical." ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RegularExpressionValidator>
            </div>

            <div class="form-group">
                <label>Username</label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter Username" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Address</label>
                <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Mobile No</label>
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile No" required></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobile" ValidationExpression="^[6-9]\d{9}$" ErrorMessage="Mobile No must be 10 digits and start with 6-9." ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" />
            </div>

            <div class="form-group">
                <label>Email</label>
                <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" required TextMode="Email"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Country</label>
                <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control" AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName">
                    <asp:ListItem Value="0">Select Country</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Country is required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CountryName] FROM [Country]"></asp:SqlDataSource>
            </div>

            <!-- Education Section -->
            <h6 class="pt-4">Education</h6>

            <div class="form-group">
                <label>10th Grade</label>
                <asp:TextBox ID="TxtTenth" runat="server" CssClass="form-control" placeholder="Ex: 90%" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label>12th Grade</label>
                <asp:TextBox ID="txtTwelth" runat="server" CssClass="form-control" placeholder="Ex: 90%" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Graduation with Percentage/Grade</label>
                <asp:TextBox ID="txtGraduation" runat="server" CssClass="form-control" placeholder="Ex: 9.2 CGPA" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Post Graduation with Percentage/Grade</label>
                <asp:TextBox ID="txtPostGraduation" runat="server" CssClass="form-control" placeholder="Mtech with 9.2" ></asp:TextBox>
            </div>

            <div class="form-group">
                <label>PHD with Percentage/Grade</label>
                <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" placeholder="PHD with Grade" ></asp:TextBox>
            </div>

            <!-- Work Experience Section -->
            <h6 class="pt-4">Work Experience</h6>

            <div class="form-group">
                <label>Works On</label>
                <asp:TextBox ID="txtWork" runat="server" CssClass="form-control" placeholder="Enter Company Name" ></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Experience</label>
                <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Ex: 2 Years" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Resume</label> 
                <asp:FileUpload ID="fuResume" runat="server" />f
                <asp:RequiredFieldValidator ID="rfvResume" runat="server" ControlToValidate="fuResume" ErrorMessage="Resume is required." ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-group">
                <asp:Button ID="btnUpdate" runat="server" Text="Update Resume" CssClass="button-contactForm" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </section>
</asp:Content>
