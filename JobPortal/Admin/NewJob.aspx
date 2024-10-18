<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NewJob.aspx.cs" Inherits="JobPortal.Admin.NewJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">
            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/JobList.aspx" CssClass="btn btn-secondary" Visible="false">< Back </asp:HyperLink>
                </div>
            </div>
            <h3 class="text-center font-weight-bold"><%: Session["title"] %></h3>
            
            <!-- Job Details Section -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtJobTitle" class="font-weight-bold">Job Title</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" placeholder="Ex: Web Developer, App Developer" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtNoOfPost" class="font-weight-bold">Number of Posts</label>
                    <asp:TextBox ID="txtNoOfPost" runat="server" CssClass="form-control" placeholder="Enter Number of Positions" TextMode="Number" required="true"></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-12">
                    <label for="txtDescription" class="font-weight-bold">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Job Description" TextMode="MultiLine" required="true"></asp:TextBox>
                </div>
            </div>

            <!-- Qualifications & Experience Section -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="TxtQualification" class="font-weight-bold">Qualification/Education</label>
                    <asp:TextBox ID="TxtQualification" runat="server" CssClass="form-control" placeholder="Ex: MCA, BTech, MBA" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtExperience" class="font-weight-bold">Experience</label>
                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Ex: 2 Years, 3 Years" required="true"></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtSpecialization" class="font-weight-bold">Specialization</label>
                    <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" placeholder="Enter Specialization" TextMode="MultiLine" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtLastDate" class="font-weight-bold">Last Date to Apply</label>
                    <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" TextMode="Date" required="true"></asp:TextBox>
                </div>
            </div>

            <!-- Salary & Job Type Section -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtSalary" class="font-weight-bold">Salary</label>
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" placeholder="Ex: 25000/Month, 7LPA" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="ddlJobType" class="font-weight-bold">Job Type</label>
                    <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Select Job Type</asp:ListItem>
                        <asp:ListItem Value="FullTime">Full Time</asp:ListItem>
                        <asp:ListItem Value="PartTime">Part Time</asp:ListItem>
                        <asp:ListItem Value="Remote">Remote</asp:ListItem>
                        <asp:ListItem Value="Freelance">Freelance</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorJobType" runat="server" ErrorMessage="Job Type is required" ForeColor="Red" ControlToValidate="ddlJobType" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Company Information Section -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtCompany" class="font-weight-bold">Company/Organization</label>
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" placeholder="Enter Company/Organization" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="fuCompanyLogo" class="font-weight-bold">Company Logo</label>
                    <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" ToolTip=".jpg, .jpeg, .png extension only"></asp:FileUpload>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtWebSite" class="font-weight-bold">Website</label>
                    <asp:TextBox ID="txtWebSite" runat="server" CssClass="form-control" placeholder="Enter Website" TextMode="Url"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtEmail" class="font-weight-bold">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" TextMode="Email"></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-12">
                    <label for="txtAddress" class="font-weight-bold">Work Location</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Work Location" TextMode="MultiLine" required="true"></asp:TextBox>
                </div>
            </div>

            <!-- Country & State Section -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="ddlCountry" class="font-weight-bold">Country</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control" AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName">
                        <asp:ListItem Value="0">Select Country</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCountry" runat="server" ErrorMessage="Country is required" ForeColor="Red" ControlToValidate="ddlCountry" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CountryName] FROM [Country]"></asp:SqlDataSource>
                </div>
                <div class="col-md-6">
                    <label for="TxtState" class="font-weight-bold">State</label>
                    <asp:TextBox ID="TxtState" runat="server" CssClass="form-control" placeholder="Enter State" required="true"></asp:TextBox>
                </div>
            </div>

            <!-- Submit Button -->
            <div class="row">
                <div class="col-md-12 text-center">
                    
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-lg mt-3" Text="Add Job" BackColor="#7200cf" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
