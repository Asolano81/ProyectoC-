<%@ Page Title="" Language="C#" MasterPageFile="~/mdiPrincipal.Master" AutoEventWireup="true" CodeBehind="frmGestGrupo.aspx.cs" Inherits="prjProyectoFinal.frmGestGrupo" %>
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
                                <h1 class="panel-title">Gestionar Grupos</h1>
                            </div>
                            <div class="panel-body">
                                <div class="container-fluid text-center">
                                    <div class="input-group">
                                        <asp:Label ID="lblAccion" runat="server" CssClass="input-group-addon" Text="Accion"></asp:Label>
                                        <asp:DropDownList ID="cbxAccion" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="cbxAccion_SelectedIndexChanged">
                                            <asp:ListItem>Seleccione</asp:ListItem>
                                            <asp:ListItem>Registrar</asp:ListItem>
                                            <asp:ListItem>Modificar</asp:ListItem>
                                            <asp:ListItem>Eliminar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblIdGrupo" runat="server" CssClass="input-group-addon" Text="Descripción Grupo"></asp:Label>
                                        <asp:TextBox ID="txtDescripcionGrupo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblDeporte" runat="server" CssClass="input-group-addon" Text="Deporte"></asp:Label>
                                        <asp:DropDownList ID="ddlDeporte" runat="server" CssClass="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                    <br />                                  
                                    <div class="input-group">
                                        <asp:Label ID="lblProfesor" runat="server" CssClass="input-group-addon" Text="Profesor"></asp:Label>
                                        <asp:DropDownList ID="ddlProfesor" runat="server" CssClass="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                    <br />
                                     <div class="input-group">
                                        <asp:Label ID="lblDias" runat="server" CssClass="input-group-addon" Text="Dias"></asp:Label>
                                        <asp:DropDownList ID="ddlDias" runat="server" CssClass="form-control" AutoPostBack="false" Enabled="false">
                                            <asp:ListItem>Seleccione</asp:ListItem>
                                            <asp:ListItem>Lunes  - Martes</asp:ListItem>
                                            <asp:ListItem>Jueves - Viernes</asp:ListItem>
                                            <asp:ListItem>Lunes  - Miercoles</asp:ListItem>
                                            <asp:ListItem>Jueves - Sábados</asp:ListItem>                                           
                                        </asp:DropDownList>                                       
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblHoraInicio" runat="server" CssClass="input-group-addon" Text="Hora Inicio"></asp:Label>
                                        <asp:DropDownList ID="ddlHoraInicio" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbxHoraInicio_SelectedIndexChanged" Width="170px" Enabled="false">
                                            <asp:ListItem>Seleccione</asp:ListItem>
                                            <asp:ListItem>06:00</asp:ListItem>
                                            <asp:ListItem>08:00</asp:ListItem>
                                            <asp:ListItem>10:00</asp:ListItem>
                                            <asp:ListItem>12:00</asp:ListItem>
                                            <asp:ListItem>14:00</asp:ListItem>
                                            <asp:ListItem>16:00</asp:ListItem>
                                            <asp:ListItem>18:00</asp:ListItem>
                                            <asp:ListItem>20:00</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblHoraFin" runat="server" CssClass="input-group-addon" Text="Hora Fin"></asp:Label>
                                        <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                            <asp:Label ID="lblInstruccion2" runat="server" CssClass="h5" Text="2. Para modificar o eliminar un grupo debe consultarlo primero con su numero de ID"></asp:Label>
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
                    <h1 class="panel-title Centrar-Medio">Grupos Agregados</h1>
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
