<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfilInativo.aspx.cs" Inherits="PickleScore.Web.Pages.Profile.PerfilInativo" MasterPageFile="~/Views/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ativar de Perfil</h2>

    <hr />

    <div style="display:flex; gap:10px">
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" OnClick="btnAtivar_Click" CssClass="btn btn-primary" />
        <%--<asp:Label ID="lblMensagemInativos" runat="server" Text=""></asp:Label>--%>
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/Pages/Profile/Perfil.aspx" CssClass="btn btn-primary" />
    </div>
    
    <hr />
    
     <asp:GridView ID="gridPerfisInativos" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
         <Columns>
             <asp:TemplateField HeaderText="Selecionar">
                 <ItemTemplate>
                     <asp:CheckBox ID="chkSelecionado" runat="server"/>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="Nome" HeaderText="Nome" />
             <asp:BoundField DataField="DataInsercao" HeaderText="Data de Inserção" />
             <asp:BoundField DataField="DataAlteracao" HeaderText="Data de Alteração" />
         </Columns>
     </asp:GridView>
</asp:Content>
