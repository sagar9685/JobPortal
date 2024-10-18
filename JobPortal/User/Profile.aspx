<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="JobPortal.User.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Add Bootstrap and Font Awesome (if not already included in the MasterPage) -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container pt-5 pb-5">
        <div class="main-body">
            <asp:DataList ID="dlprofile" runat="server" Width="100%" OnItemCommand="dlprofile_ItemCommand">
                <ItemTemplate>
                    <div class="row gutters-sm mb-4">
                        <!-- Profile Picture and Name Section -->
                        <div class="col-md-4 mb-3">
                            <div class="card shadow-sm">
                                <div class="card-body text-center">
                                  <img src="../assets/img/icon/user.jpeg" alt="UserPic" class="rounded-circle"
                                            width="150" />

                                    <h4 class="text-capitalize font-weight-bold"><%# Eval("Name") %></h4>
                                    <p class="text-secondary mb-1"><%# Eval("UserName") %></p>
                                    <p class="text-muted font-size-sm text-capitalize">
                                        <i class="fas fa-map-marker-alt mr-2"></i><%# Eval("Country") %>
                                    </p>
                                </div>
                            </div>
                        </div>

                        <!-- User Details Section -->
                        <div class="col-md-8">
                            <div class="card shadow-sm mb-3">
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-sm-3 font-weight-bold">Full Name</div>
                                        <div class="col-sm-9 text-secondary text-capitalize">
                                            <%# Eval("Name") %>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-sm-3 font-weight-bold">Email</div>
                                        <div class="col-sm-9 text-secondary">
                                            <%# Eval("Email") %>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-sm-3 font-weight-bold">Mobile</div>
                                        <div class="col-sm-9 text-secondary">
                                            <%# Eval("Mobile") %>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-sm-3 font-weight-bold">Address</div>
                                        <div class="col-sm-9 text-secondary text-capitalize">
                                            <%# Eval("Address") %>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-sm-3 font-weight-bold">Resume Upload</div>
                                        <div class="col-sm-9 text-secondary">
                                            <%# Eval("Resume") == DBNull.Value ? "Not Uploaded Resume" : "Uploaded" %>
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col-sm-12 text-right">
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit Profile" CssClass="btn btn-primary"
                                                CommandName="EditUserProfile" CommandArgument='<%# Eval("UserId") %>' />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
