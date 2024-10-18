<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="JobPortal.Admin.ContactList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <h3 class="text-center font-weight-bold">Contact List/Details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"
                        EmptyDataText="No Record To Display..!" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="ContactId" OnRowDeleting="GridView1_RowDeleting">
 

                        <columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr No." HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Name" HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-HorizontalAlign="Center" />
                            

                            




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
