<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NivelInativado.aspx.cs" Inherits="PickleScore.Web.Pages.Level.NivelInativado" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ativar Nível</h2>
    
    <hr />

    <div style="display:flex; gap:10px">
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" OnClick="btnAtivar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/Pages/Level/Nivel.aspx" CssClass="btn btn-primary" />
    </div>
    
    <hr />

    <asp:GridView ID="gridNivelInativo" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="DataInsercao" HeaderText="Data de Inserção" />
            <asp:BoundField DataField="DataAlteracao" HeaderText="Data de Alteração" />           
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-secondary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>