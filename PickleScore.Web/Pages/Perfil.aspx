<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="PickleScore.Web.Pages.Perfil" MasterPageFile="~/Views/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Perfil</h2>

    <asp:Label ID="lblNome" runat="server" Text="Nome do Perfil:"></asp:Label>
    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="300px" />
    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnInativar" runat="server" Text="Inativar" OnClick="btnInativar_Click" CssClass="btn btn-primary" />
    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

    <hr />

    <asp:GridView ID="gridPerfis" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
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

    <div id="blocoAlteracao" runat="server" visible="false">
        <asp:Label ID="lblNomeAlteracao" runat="server" Text="Nome do Perfil Alteração:"></asp:Label>
        <asp:TextBox ID="txtNomeAlteracao" runat="server" CssClass="form-control" Width="300px" />
        <asp:Button ID="btnSalvarAlteracao" runat="server" Text="Salvar" OnClick="btnSalvarAlteracao_Click" CssClass="btn btn-primary" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if (window.sucessoCadastro) {
                alert("Perfil cadastrado com sucesso!!")
            }

            $('#<%= btnSalvar.ClientID %>').click(function () {
                if (nome.trim() === "") {
                    alert("O campo Nome do Perfil é obrigatório.");
                    return false;
                }
            });
        });


    </script>
</asp:Content>
