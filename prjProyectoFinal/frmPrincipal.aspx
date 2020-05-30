<%@ Page Title="" Language="C#" MasterPageFile="~/mdiPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPrincipal.aspx.cs" Inherits="prjProyectoFinal.frmPrincipal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row Centrar-Medio">
        <asp:Label ID="lblTituloInicio" runat="server" CssClass="h3" Text="Ofrecemos la mayor calidad en nuestros centros deportivos y tenemos pasantias internacionales"></asp:Label>
    </div>
    <br />
    <div class="row Centrar-Medio">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="container-fluid text-center">
                        <div class="row Centrar-Medio">
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgFutbol" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoFutbol.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgBassketball" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoBasketball.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgNatacion" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoNatacion.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgRugby" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoRugby.jpg" Height="130px" Width="130px"/>
                            </div>
                        </div>
                        <br />
                        <div class="row Centrar-Medio">
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgAjedrez" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoAjedrez.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgBadminton" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoBadminton.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgCiclismo" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoCiclismo.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgTennis" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoTennis.jpg" Height="130px" Width="130px"/>
                            </div>
                        </div>
                        <br />  
                        <div class="row Centrar-Medio">
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgPatinaje" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoPatinaje.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgTennisMesa" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoTennisMesa.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgEsgrima" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoEsgrima.jpg" Height="130px" Width="130px"/>
                            </div>
                            <div class="col-md-4 Centrar-Medio">
                                <asp:Image ID="imgVolleyball" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/LogoVolleyball.jpg" Height="130px" Width="130px"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div> 
</asp:Content>
