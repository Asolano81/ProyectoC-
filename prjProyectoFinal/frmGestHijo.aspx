<%@ Page Title="" Language="C#" MasterPageFile="~/mdiPrincipal.Master" AutoEventWireup="true" CodeBehind="frmGestHijo.aspx.cs" Inherits="prjProyectoFinal.frmGestHijo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-body Centrar-Medio">
                    <div class="col-md-12">
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h1 class="panel-title">Gestionar Hijos</h1>
                            </div>
                            <div class="panel-body">
                                <div class="container-fluid text-center">
                                    <div class="input-group">
                                        <asp:Label ID="lblAccion" runat="server" CssClass="input-group-addon" Text="Accion"></asp:Label>
                                        <asp:DropDownList ID="cbxAccion" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="cbxAccion_SelectedIndexChanged">
                                            <asp:ListItem>...Seleccione...</asp:ListItem>
                                            <asp:ListItem>Registrar</asp:ListItem>
                                            <asp:ListItem>Modificar</asp:ListItem>
                                            <asp:ListItem>Eliminar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblUsuario" runat="server" CssClass="input-group-addon" Text="Usuario"></asp:Label>
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblEmail" runat="server" CssClass="input-group-addon" Text="Email"></asp:Label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblContraseña" runat="server" CssClass="input-group-addon" Text="Contraseña"></asp:Label>
                                        <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblIdentificacion" runat="server" CssClass="input-group-addon" Text="Identificacion"></asp:Label>
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblNombres" runat="server" CssClass="input-group-addon" Text="Nombres"></asp:Label>
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblApellidos" runat="server" CssClass="input-group-addon" Text="Apellidos"></asp:Label>
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblFecNac" runat="server" CssClass="input-group-addon" Text="Fecha de Nacimiento"></asp:Label>
                                        <asp:TextBox ID="txtFecNac" runat="server" CssClass="form-control" TextMode="Date" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblTelefono" runat="server" CssClass="input-group-addon" Text="Telefono"></asp:Label>
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblIdPadre" runat="server" CssClass="input-group-addon" Text="Identificación Padre"></asp:Label>
                                        <asp:TextBox ID="txtIdPadre" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <asp:Panel ID="pnlRegistrar" runat="server" Visible="false">
                                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="btnRegistrar_Click" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlModificar" runat="server" Visible="false">
                                        <asp:Button ID="btnConsultar1" runat="server" CssClass="btn btn-success" Text="Consultar" OnClick="btnConsultar1_Click" />
                                        <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-success" Text="Modificar" Enabled="false" OnClick="btnModificar_Click" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlEliminar" runat="server" Visible="false">
                                        <asp:Button ID="btnConsultar2" runat="server" CssClass="btn btn-success" Text="Consultar" OnClick="btnConsultar2_Click" />
                                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-success" Text="Eliminar" Enabled="false" OnClick="btnEliminar_Click" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-body center-block">
                    <div class="container-fluid text-justify">
                        <div class="row"> 
                            <asp:Label ID="lblTitulo" runat="server" CssClass="h5" Text="Instrucciones:" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="row"> 
                            <asp:Label ID="lblInstruccion1" runat="server" CssClass="h5" Text="1. Seleccione la acción a realizar"></asp:Label>
                        </div>
                        <div class="row"> 
                            <asp:Label ID="lblInstruccion2" runat="server" CssClass="h5" Text="2. Para modificar o eliminar un hijo registrado debe consultarlo primero con la identificación"></asp:Label>
                        </div>
                        <br />
                        <div class="row Centrar-Medio">
                            <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                                <div class="input-group">
                                    <div class="alert alert-info" role="alert">
                                        <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title Centrar-Medio">Estudiantes Registrados</h1>
                </div>
                <div class="panel panel-default">
                    <div class="panel-body center-block">
                        <br />
                        <div class="container-fluid Centrar-Medio">
                            <asp:GridView ID="gvMatricula" runat="server" CssClass="table table-responsive table-bordered table-condensed table-hover"></asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
