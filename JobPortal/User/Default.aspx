<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JobPortal.User.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <style>
        .input-group .form-control {
            height: 48px; /* Set the height to your preferred value */
        }

        .input-group .btn {
            height: 48px; /* Match the height of the input field */
            padding: 0 15px; /* Add some padding for better appearance */
            display: flex; /* Ensure the button uses flexbox */
            align-items: center; /* Center the content vertically */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="container">

        <!-- Slider Area Start -->
        <div class="slider-area mb-4">
            <div class="slider-active">
                <div class="single-slider slider-height d-flex align-items-center" style="background-image: url('../assets/img/hero/h1_hero.jpg');">
                    <div class="container">
                        <div class="row">
                            <div class="col-xl-6 col-lg-9 col-md-10">
                                <div class="hero__caption text-light">
                                    <h1 class="display-4">Find the most exciting startup jobs</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Slider Area End -->

        <!-- Job Listing Area Start -->
        <div class="job-listing-area pb-5">
            <div class="row justify-content-center">

                <!-- Right Content -->
                <div class="col-xl-12 col-lg-12 col-md-12">
                    <section class="">
                        <div class="container">
                            <div class="row mb-4">
                                <div class="col-lg-12">
                                    <div class="section-tittle text-center">
                                        <span>Your Recent Jobs</span>
                                        <h2 class="text-primary">My Jobs</h2>
                                    </div>
                                </div>
                            </div>

                            <!-- Search Bar Section -->
                            <div class="row mb-4 justify-content-center">
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Search for jobs..."></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <asp:Label ID="Label1" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <!-- End Search Bar Section -->

                            <!-- Not Found Label Section -->
                            <div class="row justify-content-center">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblNotFound" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <!-- End Not Found Label Section -->

                            <!-- Job Listings Section -->
                            <div class="row justify-content-center">
                                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" CssClass="job-list">
                                    <ItemTemplate>
                                        <div class="single-job-items mb-30 p-3 border rounded shadow-sm mx-auto">
                                            <div class="job-items d-flex align-items-center">
                                                <div class="company-img me-3">
                                                    <a href="JobDetails.aspx?id=<%# Eval("JobId") %>">
                                                        <img width="80" src="<%# GetImageUrl(Eval("CompanyImage")) %>" alt="">
                                                    </a>
                                                </div>
                                                <div class="job-tittle">
                                                    <a href="JobDetails.aspx?id=<%# Eval("JobId") %>">
                                                        <h5 class="mb-1"><%# Eval("Title") %></h5>
                                                    </a>
                                                    <ul class="list-unstyled">
                                                        <li class="text-muted"><%# Eval("CompanyName") %></li>
                                                        <li class="text-muted"><i class="fas fa-map-marker-alt"></i><%# Eval("State") %>, <%# Eval("Country") %></li>
                                                        <li class="text-muted"><i class="fas fa-dollar-sign"></i><%# Eval("Salary") %></li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="items-link items-link2 f-right">
                                                <a href="JobDetails.aspx?id=<%# Eval("JobId") %>" class="btn btn-outline-primary btn-sm"><%# Eval("JobType") %></a>
                                                <span class="text-secondary">
                                                    <i class="fas fa-clock pr-1"></i>
                                                    <%# RelativeDate(Convert.ToDateTime(Eval("CreateDate"))) %>
                                                </span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
        <!-- Job Listing Area End -->

        <!-- Online CV Area Start -->
        <div class="online-cv cv-bg section-overly pt-5 pb-5" style="background-image: url('../assets/img/gallery/cv_bg.jpg');">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-xl-10">
                        <div class="cv-caption text-center text-white">
                            <p class="pera1">FEATURED TOURS Packages</p>
                            <p class="pera2">Make your own trip with our tours</p>
                            <a href="Profile.aspx" class="btn btn-success">Upload your CV</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Online CV Area End -->

        <!-- Apply Process Caption -->
        <div class="apply-process-area pt-5 pb-5">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-6">
                        <div class="single-process text-center mb-30">
                            <div class="process-ion">
                                <span class="flaticon-search"></span>
                            </div>
                            <div class="process-cap">
                               <h5>1. Search a job</h5>
                               <p>Sorem spsum dolor sit amsectetur adipisclit, seddo eiusmod tempor incididunt ut laborea.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="single-process text-center mb-30">
                            <div class="process-ion">
                                <span class="flaticon-curriculum-vitae"></span>
                            </div>
                            <div class="process-cap">
                               <h5>2. Apply for job</h5>
                               <p>Sorem spsum dolor sit amsectetur adipisclit, seddo eiusmod tempor incididunt ut laborea.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="single-process text-center mb-30">
                            <div class="process-ion">
                                <span class="flaticon-tour"></span>
                            </div>
                            <div class="process-cap">
                               <h5>3. Get your job</h5>
                               <p>Sorem spsum dolor sit amsectetur adipisclit, seddo eiusmod tempor incididunt ut laborea.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- How Apply Process End-->

        <!-- Testimonial Start -->
        <div class="testimonial-area testimonial-padding">
            <div class="container">
                <!-- Testimonial contents -->
                <div class="row d-flex justify-content-center">
                    <div class="col-xl-8 col-lg-8 col-md-10">
                        <div class="h1-testimonial-active dot-style">
                            <!-- Single Testimonial -->
                            <div class="single-testimonial text-center">
                                <!-- Testimonial Content -->
                                <div class="testimonial-caption ">
                                    <!-- founder -->
                                    <div class="testimonial-founder  ">
                                        <div class="founder-img mb-30">
                                            <img src="../assets/img/testmonial/testimonial-founder.png" alt="">
                                            <span>Margaret Lawson</span>
                                            <p>Creative Director</p>
                                        </div>
                                    </div>
                                    <div class="testimonial-top-cap">
                                        <p>“I am at an age where I just want to see progress in my health today!”</p>
                                    </div>
                                </div>
                            </div>
                            <!-- End Single Testimonial -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Testimonial End -->

    </main>
</asp:Content>
