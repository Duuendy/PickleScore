<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Campeonato.aspx.cs" Inherits="PickleScore.Web.Pages.Championship.Campeonato" MasterPageFile="~/Views/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Campeonato</h2>
    <hr />

    <div style="display:flex; gap: 8px">
        <button id="btnNovoCampeonato" type="button" Class="btn btn-primary">Novo Campeonato</button>
    </div>
    <hr />

    <asp:GridView ID="gridCampeonato" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table" OnRowCommand="abrirModal">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Local" HeaderText="Local" />
            <asp:BoundField DataField="DataInicio" HeaderText="Data Inicio" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:BoundField DataField="DataFim" HeaderText="Data Fim" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-secondary"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!--Modal-->
    <div class="modal fade" id="modalCampeonato" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Editar Campeonato</h5>
                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:TextBox ID="txtNome" placeholder="Nome" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txtLocal" placeholder="Local" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txtDataInicio" placeholder="Data Início" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txtDataFim" placeholder="Data Fim" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvarCampeonato" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-secondary"/>
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
        $(document).ready(function () {
            $('#btnNovoCampeonato').click(function () {
                $('#modalCampeonato').modal('show');
            });
        });
    </script>
</asp:Content>
