<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewResume.aspx.cs" Inherits="JobPortal.Admin.NewResume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <h3 class="text-center font-weight-bold">View Resume/Download Resume</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"
                        EmptyDataText="No Record To Display..!" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="AppliedJobId" OnRowDeleting="GridView1_RowDeleting"
                       OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">

                        <columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr No." HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Title" HeaderText="Job Title" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Name" HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Email" HeaderText="User Email" HeaderStyle-HorizontalAlign="Center" />
                           
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile No." HeaderStyle-HorizontalAlign="Center" />
                            
                            
                            <asp:TemplateField HeaderText="Resume">
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" 
    NavigateUrl='<%# DataBinder.Eval(Container,"DataItem.Resume","../{0}") %>' 
    CssClass="btn btn-primary">
    <i class="fas fa-download"></i> Download
</asp:HyperLink>
        <asp:HiddenField ID="hdnJobId" runat="server" value='<%# Eval("JobId") %>' Visible="false" />

        
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
