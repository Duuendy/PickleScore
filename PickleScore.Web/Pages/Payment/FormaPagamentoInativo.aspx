<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormaPagamentoInativo.aspx.cs" Inherits="PickleScore.Web.Pages.Payment.FormaPagamentoInativo" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Ativar Forma De Pagamento</h2>
    <hr />

    <div style="display:flex; gap:10px">
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" OnClick="btnAtivar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/Pages/Payment/FormaPagamento.aspx" CssClass="btn btn-primary" />
    </div>
    <hr />

    <asp:GridView ID="gridFormaPagamentoInativo" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
        </Columns>
    </asp:GridView>
</asp:Content>


