﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using NewLife;
    
    #line 1 "..\..\Areas\Admin\Views\User\Login.cshtml"
    using NewLife.Common;
    
    #line default
    #line hidden
    using NewLife.Cube;
    using NewLife.Reflection;
    using NewLife.Web;
    using XCode;
    using XCode.Membership;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/User/Login.cshtml")]
    public partial class _Areas_Admin_Views_User_Login_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Admin_Views_User_Login_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\Admin\Views\User\Login.cshtml"
  
    Layout = null;
    ViewBag.Title = "登录";

            
            #line default
            #line hidden
WriteLiteral("\r\n<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"zh-CN\"");

WriteLiteral(">\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1\"");

WriteLiteral(" />\r\n    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->\r\n\r\n    <title>");

            
            #line 14 "..\..\Areas\Admin\Views\User\Login.cshtml"
      Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" - ");

            
            #line 14 "..\..\Areas\Admin\Views\User\Login.cshtml"
                       Write(SysConfig.Current.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 419), Tuple.Create("\"", 439)
, Tuple.Create(Tuple.Create("", 426), Tuple.Create<System.Object, System.Int32>(Href("~/favicon.ico")
, 426), false)
);

WriteLiteral(" rel=\"shortcut icon\"");

WriteLiteral(" type=\"image/x-icon\"");

WriteLiteral(" />\r\n\r\n    <!-- 基本样式 -->\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 515), Tuple.Create("\"", 563)
, Tuple.Create(Tuple.Create("", 522), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap/css/bootstrap.min.css")
, 522), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <!-- 自定义样式-->\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 614), Tuple.Create("\"", 639)
, Tuple.Create(Tuple.Create("", 621), Tuple.Create<System.Object, System.Int32>(Href("~/Content/Cube.css")
, 621), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <!-- 仅用于过渡期兼容-->\r\n    <style>\r\n        .login-logo { width: 130px; font-" +
"size: 130px; color: #4CA6FF; margin-top: 50px; }\r\n        .cube-login { backgrou" +
"nd: #fff; padding-bottom: 40px; border-radius: 15px; text-align: center; }\r\n    " +
"    .cube-login .heading { display: block; font-size: 24px; font-weight: 700; pa" +
"dding: 5px 0; margin-bottom: 20px; }\r\n        .cube-login .form-group { padding:" +
" 0 40px; margin: 0 0 25px 0; position: relative; display: block; }\r\n        .cub" +
"e-login .form-control { border-radius: 20px; box-shadow: none; padding: 0 20px 0" +
" 45px; height: 40px; transition: all 0.3s ease 0s; }\r\n        .cube-login .form-" +
"control:focus { background: #e0e0e0; box-shadow: none; outline: 0 none; }\r\n     " +
"   .cube-login .form-group i { position: absolute; top: 12px; left: 60px; font-s" +
"ize: 17px; color: #c8c8c8; transition: all 0.5s ease 0s; }\r\n        .cube-login " +
".form-group a { position: absolute; top: 12px; right: 0px; font-size: 17px; colo" +
"r: #c8c8c8; transition: all 0.5s ease 0s; color: #4CA6FF; }\r\n        .cube-login" +
" .form-control:focus + i { color: #00b4ef; }\r\n        .cube-login .fa-question-c" +
"ircle { display: inline-block; position: absolute; top: 12px; right: 60px; font-" +
"size: 20px; color: #808080; transition: all 0.5s ease 0s; }\r\n        .cube-login" +
" .fa-question-circle:hover { color: #000; }\r\n        .cube-login .main-checkbox " +
"{ float: left; width: 20px; height: 20px; background: #11a3fc; border-radius: 50" +
"%; position: relative; margin: 5px 0 0 5px; border: 1px solid #11a3fc; }\r\n      " +
"  .cube-login .main-checkbox label { width: 20px; height: 20px; position: absolu" +
"te; top: 0; left: 0; cursor: pointer; }\r\n        .cube-login .main-checkbox labe" +
"l:after { content: \"\"; width: 10px; height: 5px; position: absolute; top: 5px; l" +
"eft: 4px; border: 3px solid #fff; border-top: none; border-right: none; backgrou" +
"nd: transparent; opacity: 0; -webkit-transform: rotate(-45deg); transform: rotat" +
"e(-45deg); }\r\n        .cube-login .main-checkbox input[type=checkbox] { visibili" +
"ty: hidden; }\r\n        .cube-login .main-checkbox input[type=checkbox]:checked +" +
" label:after { opacity: 1; }\r\n        .cube-login .text { float: left; margin-le" +
"ft: 7px; line-height: 20px; padding-top: 5px; text-transform: capitalize; }\r\n   " +
"     .cube-login .btn { float: right; font-size: 14px; color: #fff; background: " +
"#00b4ef; border-radius: 30px; padding: 8px 50px; border: none; text-transform: c" +
"apitalize; transition: all 0.5s ease 0s; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <" +
"!--布局容器-->\r\n    <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-6 col-md-offset-3\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"tab-content\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 49 "..\..\Areas\Admin\Views\User\Login.cshtml"
               Write(Html.Partial("_Login_Login"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 50 "..\..\Areas\Admin\Views\User\Login.cshtml"
               Write(Html.Partial("_Login_Forgot"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 51 "..\..\Areas\Admin\Views\User\Login.cshtml"
               Write(Html.Partial("_Login_Register"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!-" +
"- JQuery作为一等公民，页面内部随时可能使用 -->\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 3579), Tuple.Create("\"", 3617)
, Tuple.Create(Tuple.Create("", 3585), Tuple.Create<System.Object, System.Int32>(Href("~/Content/js/jquery-2.1.3.min.js")
, 3585), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 3641), Tuple.Create("\"", 3686)
, Tuple.Create(Tuple.Create("", 3647), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap/js/bootstrap.min.js")
, 3647), false)
);

WriteLiteral(@"></script>
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src=""https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js""></script>
      <script src=""https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js""></script>
    <![endif]-->

</body>
</html>");

        }
    }
}
#pragma warning restore 1591
