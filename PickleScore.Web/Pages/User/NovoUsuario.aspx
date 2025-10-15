<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovoUsuario.aspx.cs" Inherits="PickleScore.Web.Pages.User.NovoUsuario" MasterPageFile="~/Views/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .container{
            background:gray;
        }
    </style>
    <h2>Novo Usuário</h2>
    
    <div class="container">
        <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label>
        <br />
        <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblSobrenome" runat="server" Text="SobreNome"></asp:Label>
        <br />
        <asp:TextBox ID="txtSobrenome" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblCpf" runat="server" Text="CPF"></asp:Label>
        <br />
        <asp:TextBox ID="txtCpf" runat="server"></asp:TextBox>    
        <br />
        <br />
        <asp:Label ID="lblSenha" runat="server" Text="Senha"></asp:Label>
        <br />
        <asp:TextBox ID="txtSenha" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblEmail" runat="server" Text="E-Mail"></asp:Label>
        <br />
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblNascimento" runat="server" Text="Data Nascimento"></asp:Label>
        <br />
        <asp:TextBox ID="txtNascimento" runat="server"></asp:TextBox>
        <br />
        <br />
       
        
<%--        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click"/>--%>
        <hr />

    </div>
</asp:Content>
