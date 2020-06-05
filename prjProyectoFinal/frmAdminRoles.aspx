<%@ Page Title="" Language="C#" MasterPageFile="~/mdiPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAdminRoles.aspx.cs" Inherits="prjProyectoFinal.frmAdminRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-body Centrar-Medio">
                    <div class="col-md-8">
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h1 class="panel-title">Administrar Roles</h1>
                            </div>
                            <div class="panel-body">
                                <br />
                                <div class="container-fluid text-center">
                                    <div class="input-group">
                                        <asp:Label ID="lblIdentificacion" runat="server" CssClass="input-group-addon" Text="Cedula del Usuario"></asp:Label>
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                    </div>
                                    <br />
                                    <asp:Button ID="btnConsultar" runat="server" CssClass="btn btn-success" Text="Consultar" OnClick="btnConsultar_Click" />
                                    <br />
                                    <asp:Panel ID="pnlRegistrar" runat="server" Visible="true">
                                        <br />
                                        <div class="input-group">
                                            <asp:Label ID="lblRol" runat="server" CssClass="input-group-addon" Text="Añadir Rol"></asp:Label>
                                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                        <br />
                                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-success" Text="Registrar" Enabled="false" OnClick="btnRegistrar_Click" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title Centrar-Medio">Roles del Usuario</h1>
                </div>
                <div class="panel panel-default">
                    <div class="panel-body center-block">
                        <br />
                        <div class="container-fluid Centrar-Medio">
                            <asp:GridView ID="gvMatricula" runat="server" CssClass="table table-responsive table-bordered table-condensed table-hover"></asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                    <div class="input-group Centrar-Medio">
                        <div class="alert alert-info" role="alert">
                            <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
