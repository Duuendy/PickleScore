<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioInativo.aspx.cs" Inherits="PickleScore.Web.Pages.User.UsuarioInativo" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ativar de Usuário</h2>

    <asp:Button ID="btnAtivar" runat="server" Text="Ativar" OnClick="btnAtivar_Click" CssClass="btn btn-primary" />
    <%--<asp:Label ID="lblMensagemInativos" runat="server" Text=""></asp:Label>--%>
    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/Pages/User/Usuario.aspx" CssClass="btn btn-primary" />
    
    <hr />
    
     <asp:GridView ID="gridUsuariosInativos" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
         <Columns>
             <asp:TemplateField HeaderText="Selecionar">
                 <ItemTemplate>
                     <asp:CheckBox ID="chkSelecionado" runat="server"/>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="Nome" HeaderText="Nome" />
             <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" />
             <asp:BoundField DataField="Cpf" HeaderText="CPF" />
             <asp:BoundField DataField="Email" HeaderText="E-Mail" />
             <asp:BoundField DataField="Nascimento" HeaderText="Data Nascimento" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"/>
             <asp:BoundField DataField="PerfilId" HeaderText="Perfil" />
             <asp:BoundField DataField="UsuarioInsercao" HeaderText="Usuario Inserção" />
             <asp:BoundField DataField="DataInsercao" HeaderText="Data de Inserção" />
             <asp:BoundField DataField="UsuarioAlteracao" HeaderText="Usuario Alteração" />
             <asp:BoundField DataField="DataAlteracao" HeaderText="Data de Alteração" />    
         </Columns>
     </asp:GridView>
</asp:Content>
