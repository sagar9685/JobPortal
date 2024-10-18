<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="JobPortal.Admin.JobList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <%--<div>
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>
            </div>--%>
            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                     <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                </div>
                <div class="input-group h-25">
    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn alert-secondary" Visible="false">< Back</asp:HyperLink>

</div>
            </div>
            
            <h3 class="text-center font-weight-bold">Job List/Details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"
                        EmptyDataText="No Record To Display..!" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="JobId" OnRowDeleting="GridView1_RowDeleting"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" >

                        <columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr No." HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Title" HeaderText="Job Title" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="NoOfPost" HeaderText="No Of Post" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Qualification" HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Experience" HeaderText="Experience" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="LastDateToApply" HeaderText="Valid Till" DataFormatString="{0:dd MMMM yyyy}" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Country" HeaderText="Country" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CreateDate" HeaderText="Posted Date" DataFormatString="{0:dd MMMM yyyy}" HeaderStyle-HorizontalAlign="Center" />

                            <asp:TemplateField HeaderText="Edit">
    <ItemTemplate>
        <asp:LinkButton
            ID="btnEditJob"
            runat="server"
            CommandName="EditJob"
            CommandArgument='<%# Eval("JobId") %>'>
            <asp:Image ID="Img" runat="server" ImageUrl="../assets/img/icon/Edit.png" Height="25px" Width="25px" ToolTip="Update" CssClass="update-button-css" />
        </asp:LinkButton>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>




                            <asp:TemplateField HeaderText="Delete">
                                <itemtemplate>
                                    <asp:ImageButton
                                        ID="btnDelete"
                                        runat="server"
                                        ImageUrl="../assets/img/icon/Trashicon.png"
                                        CommandName="Delete"
                                        Height="25px"
                                        Width="25px"
                                        ToolTip="Delete"
                                        CssClass="delete-button-css" />
                                </itemtemplate>
                                <itemstyle horizontalalign="Center" />
                            </asp:TemplateField>
                        </columns>

                        <headerstyle backcolor="#7200cf" forecolor="White" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
