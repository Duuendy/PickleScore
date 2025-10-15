<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PickleScore.Web.Pages.Login.Login" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 60px); /* Considerando o cabeçalho */
            background: #f9f9fb;
        }

        .login-card {
            display: flex;
            align-items: center;
            background: #fff;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            gap: 40px;
        }

        .login-image img {
            width: 280px;
            height: auto;
            border-radius: 8px;
        }

        .login-box {
            display: flex;
            flex-direction: column;
            width: 300px;
            text-align: center;
        }

        .login-box h2 {
            margin-bottom: 20px;
            font-size: 22px;
            font-weight: bold;
        }

        .input-group {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 8px;
            background: #f7f7f7;
        }

        .input-group i {
            margin-right: 8px;
            color: #888;
        }

        .form-control {
            border: none;
            outline: none;
            flex: 1;
            background: transparent;
            font-size: 14px;
        }

        .btn-login {
            background: #7b4df3;
            color: white;
            border: none;
            padding: 10px;
            width: 100%;
            border-radius: 8px;
            font-size: 16px;
            margin-top: 10px;
            cursor: pointer;
        }

        .btn-login:hover {
            background: #693ed1;
        }

        .options {
            text-align: right;
            margin-bottom: 10px;
        }

        .register-link {
            margin-top: 15px;
            font-size: 14px;
        }

        .register-link a {
            color: #7b4df3;
            text-decoration: none;
            font-weight: bold;
        }

        /* Responsividade */
        @media (max-width: 768px) {
            .login-card {
                flex-direction: column;
                text-align: center;
            }

            .login-image img {
                width: 200px;
                margin-bottom: 20px;
            }
        }
    </style>

    <div class="login-container">
        <div class="login-card">
            <div class="login-image">
                <img src="../../Content/imagens/login-img.png" alt="Pickleball" />
            </div>

            <div class="login-box">

                <div class="input-group">
                    <i class="uil uil-user"></i>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="E - Mail"></asp:TextBox>
                </div>

                <div class="input-group">
                    <i class="uil uil-lock"></i>
                    <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" TextMode="Password" placeholder="Senha"></asp:TextBox>

                </div>

                <div class="options">
                    <asp:HyperLink ID="lnkEsqueceuSenha" runat="server" NavigateUrl="#">Esqueceu a Senha</asp:HyperLink>
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn-login" onClick="btnAcessar_Click" />

                <p class="register-link">
                    Não tem uma conta? <a href="../User/Usuario.aspx"       >Cadastrar</a>
                </p>
            </div>
        </div>
    </div>
    
</asp:Content>

    