<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="PickleScore.Web.Pages.Category.Categoria" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Categoria</h2>

    <hr />
  
    <asp:Label ID="lblNome" runat="server" Text="Nome da Categoria"></asp:Label>
    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="300px" />
    
    <hr />
    
    <div style="display:flex; gap:10px">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnInativar" runat="server" Text="Inativar"  OnClick="btnInativar_Click" CssClass="btn btn-primary" />
        <%--<asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>--%>
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" PostBackUrl="~/Pages/Category/CategoriaInativo.aspx" CssClass="btn btn-secondary" />
    </div>
    
    <hr />

    <asp:GridView ID="gridCategorias" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
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

    <!--Alerta-->
    <div id="divMensagem" class="alert alert-success alert-dismissible fade d-none" role="alert">
        <span id="lblMensagemTexto"></span>
        <button type="button" class="btn-close" data-dismiss="alert" aria-label="Fechar"></button>
    </div>

</asp:Content>
