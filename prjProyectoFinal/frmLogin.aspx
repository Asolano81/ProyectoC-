<%@ Page Title="" Language="C#" MasterPageFile="~/mdiPrincipal.Master" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="prjProyectoFinal.frmLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row Centrar-Medio">
        <div class="col-md-12">
            <div class="panel panel-default">
                <br />
                <br />
                <div class="panel-body Centrar-Medio">
                    <div class="col-md-4">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h1 class="panel-title">Iniciar sesión</h1>
                            </div>
                            <div class="panel-body">
                                <div class="container-fluid text-center">
                                    <div class="input-group">
                                        <asp:Label ID="lblUsuario" runat="server" CssClass="input-group-addon" Text="Usuario"></asp:Label>
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblContraseña" runat="server" CssClass="input-group-addon" Text="Contraseña"></asp:Label>
                                        <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblRol" runat="server" CssClass="input-group-addon" Text="Rol de Aceeso"></asp:Label>
                                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <br />
                                    <asp:Button ID="btnIniciar" runat="server" CssClass="btn btn-success" Text="Iniciar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>  
</asp:Content>
