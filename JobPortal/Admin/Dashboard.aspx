<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="JobPortal.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css" />

    <style>
        .card {
            background-color: #fff;
            border-radius: 10px;
            border: none;
            margin-bottom: 30px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease-in-out;
        }

        .card:hover {
            transform: translateY(-10px);
        }

        .l-bg-cherry {
            background: linear-gradient(to right, #ff6b6b, #f09) !important;
            color: #fff;
        }

        .l-bg-blue-dark {
            background: linear-gradient(to right, #373b44, #4286f4) !important;
            color: #fff;
        }

        .l-bg-green-dark {
            background: linear-gradient(to right, #0a504a, #38ef7d) !important;
            color: #fff;
        }

        .l-bg-orange-dark {
            background: linear-gradient(to right, #ff8c00, #ffba56) !important;
            color: #fff;
        }

        .card-icon {
            font-size: 60px;
            text-align: center;
            color: rgba(255, 255, 255, 0.8);
            position: absolute;
            top: -30px;
            right: 10px;
        }

        .card-title {
            font-size: 18px;
            font-weight: bold;
            text-transform: uppercase;
            margin-bottom: 15px;
        }

        .card h2 {
            font-size: 36px;
            margin: 0;
        }

        .dashboard-container {
            padding-top: 30px;
        }

        .row-align-items-center {
            justify-content: center;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container dashboard-container">
        <div class="row">
            <div class="col-12 text-center pb-3">
                <h2>Admin Dashboard</h2>
            </div>
        </div>
        <div class="row justify-content-center">
            <!-- Total Jobs -->
            <div class="col-xl-3 col-lg-6 col-md-6 mb-4">
                <div class="card l-bg-cherry">
                    <div class="p-4">
                        <div class="card-icon">
                            <i class="fas fa-briefcase"></i>
                        </div>
                        <h5 class="card-title">Total User</h5>
                        <h2><%Response.Write(Session["Users"]); %></h2>
                    </div>
                </div>
            </div>

            <!-- Applied Jobs -->
            <div class="col-xl-3 col-lg-6 col-md-6 mb-4">
                <div class="card l-bg-blue-dark">
                    <div class="p-4">
                        <div class="card-icon">
                            <i class="fas fa-check-square"></i>
                        </div>
                        <h5 class="card-title">Total Jobs</h5>
                        <h2><%Response.Write(Session["Jobs"]); %></h2>
                    </div>
                </div>
            </div>

            <!-- Contact Users -->
            <div class="col-xl-3 col-lg-6 col-md-6 mb-4">
                <div class="card l-bg-green-dark">
                    <div class="p-4">
                        <div class="card-icon">
                            <i class="fas fa-comments"></i>
                        </div>
                        <h5 class="card-title">Applied Jobs</h5>
                        <h2><%Response.Write(Session["AppliedJobs"]); %></h2>
                    </div>
                </div>
            </div>

            <!-- Pending Jobs -->
            <div class="col-xl-3 col-lg-6 col-md-6 mb-4">
                <div class="card l-bg-orange-dark">
                    <div class="p-4">
                        <div class="card-icon">
                            <i class="fas fa-tasks"></i>
                        </div>
                        <h5 class="card-title">Contact Users</h5>
                        <h2><%Response.Write(Session["Contact"]); %></h2>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
