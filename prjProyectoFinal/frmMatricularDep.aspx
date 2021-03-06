﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mdiPrincipal.Master" AutoEventWireup="true" CodeBehind="frmMatricularDep.aspx.cs" Inherits="prjProyectoFinal.frmMatricularDep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-body Centrar-Medio">
                    <div class="col-md-10">
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h1 class="panel-title">Matricular Deportes</h1>
                            </div>
                            <div class="panel-body">
                                <div class="container-fluid text-center">
                                    <div class="input-group">
                                        <asp:Label ID="lblAccion" runat="server" CssClass="input-group-addon" Text="Accion"></asp:Label>
                                        <asp:DropDownList ID="cbxAccion" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="cbxAccion_SelectedIndexChanged">
                                            <asp:ListItem>...Seleccione...</asp:ListItem>
                                            <asp:ListItem>Agregar</asp:ListItem>
                                            <asp:ListItem>Modificar</asp:ListItem>
                                            <asp:ListItem>Eliminar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>                                   
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblIdEstudiante" runat="server" CssClass="input-group-addon" Text="Identificacion Estudiante"></asp:Label>
                                        <asp:TextBox ID="txtIdentEstu" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblFecDeMatr" runat="server" CssClass="input-group-addon" Text="Fecha de Matricula"></asp:Label>
                                        <asp:TextBox ID="txtFechaDeMatr" runat="server" CssClass="form-control" TextMode="Date" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblDeporte" runat="server" CssClass="input-group-addon" Text="Deporte"></asp:Label>
                                        <asp:DropDownList ID="ddlDeporte" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlDeporte_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblGrupo" runat="server" CssClass="input-group-addon" Text="Grupo"></asp:Label>
                                        <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblProfesor" runat="server" CssClass="input-group-addon" Text="Profesor"></asp:Label>
                                        <asp:TextBox ID="txtProfesor" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblDias" runat="server" CssClass="input-group-addon" Text="Días"></asp:Label>
                                        <asp:TextBox ID="txtDias" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:Label ID="lblHoraInicio" runat="server" CssClass="input-group-addon" Text="Hora Inicio"></asp:Label>
                                        <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                            <asp:Label ID="lblInstruccion2" runat="server" CssClass="h5" Text="2. El ID Matricula corresponde a tu número de identificación"></asp:Label>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblInstruccion3" runat="server" CssClass="h5" Text="3. Para modificar o eliminar un deporte de la matricula debes consultarlo primero con ID Matricula y el deporte que se desea modificar o eliminar"></asp:Label>
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
                    <h1 class="panel-title Centrar-Medio">Deportes Matriculados</h1>
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
