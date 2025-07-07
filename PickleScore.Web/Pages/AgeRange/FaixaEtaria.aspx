<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaixaEtaria.aspx.cs" Inherits="PickleScore.Web.Pages.AgeRange.FaixaEtaria" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Faixa Etária</h2>
    <hr />

    <asp:Label ID="lblNome" runat="server" Text="Faixa Etária"></asp:Label>
    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
    <hr />

    <div style="display:flex; gap:10px">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnInativar" runat="server" Text="Inativar" OnClick="btnInativar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" PostBackUrl="~/Pages/AgeRange/FaixaEtariaInativada.aspx" CssClass="btn btn-secondary" />
    </div>
    <hr />

    <asp:GridView ID="gridFaixaEtaria" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table" OnRowCommand="abrirModalEdicao">
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
                    <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-secondary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>     

    <!--Modal-->
    <div class="modal fade" id="modalFaixaEtaria" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Editar Faixa Etária</h5>
                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:TextBox ID="txtNomeModal" placeholder="Nome" runat="server" CssClass="form-control" />
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvarFaixa" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
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
            $('#modalNivel').modal('show');
        });
    </script>

</asp:Content>