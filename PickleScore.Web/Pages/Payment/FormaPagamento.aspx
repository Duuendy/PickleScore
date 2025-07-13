<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormaPagamento.aspx.cs" Inherits="PickleScore.Web.Pages.Payment.FormaPagamento" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro Forma de Pagamento</h2>
    <hr />

    <asp:Label ID="lblNome" runat="server" Text="Forma De Pagamento"></asp:Label>
    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
    <hr />

    <div style="display:flex; gap:10px">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnInativar" runat="server" Text="Inativar" OnClick="btnInativar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" PostBackUrl="~/Pages/Payment/FormaPagamentoInativo.aspx" CssClass="btn btn-secondary" />
    </div>
    <hr />

    <asp:GridView ID="gridFormaPagamento" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="abrirModalEdicao">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-secondary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!--Modal-->
    <div class="modal fade" id="modalFormaPagamento" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Editar Forma De Pagamento</h5>
                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:TextBox ID="txtNomeModal" placeholder="Nome" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvarFormaPag" runat="server" Text="Salvar" OnClientClick="btnSalvar_Click" CssClass="btn-secondary"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!--Alerta-->
    <div id="divMensagem" class="alert alert-success alert-dismissible fade d-none" role="alert">
        <span id="lblMensagemTexto"></span>
        <button type="button" class="btn-close" data-dismiss="alert" aria-label="Fechar"></button>
    </div>

    <script>
        $('#btnEditar').click(function () {
            $('#modalFormaPagamento').modal("show");
        });
    </script>
</asp:Content>
