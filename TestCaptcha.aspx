<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestCaptcha.aspx.cs" Inherits="TestCaptcha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="Captcha();">
    <form id="form1" runat="server">
    <div>
     <table>
          <tr>
           <td>
                 Text Captcha<br />
           </td>
          </tr>
          <tr>
           <td>
             <%--<input type="text" id="mainCaptcha"/>--%>
               <div id="mainCaptcha" style="width:100px;height:40px;"></div>
              <input type="button" id="refresh" value="Refresh" onclick="Captcha();" />
           </td>
          </tr>
          <tr>
           <td>
            <input type="text" id="txtInput"/>    
          </td>
         </tr>
         <tr>
          <td>
            <input id="Button1" type="button" value="Check" onclick="alert(ValidCaptcha());"/>
          </td>
        </tr>
      </table>
    </div>
        <style>
            #mainCaptcha > span{                
                position: absolute;
                color: #000000;
                border:1px solid;
            }
            #mainCaptcha{
                width:100px;
                height:40px;
            }
        </style>
        <script type="text/javascript">
                 function Captcha(){
                     var alpha = new Array('A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z');
                     var i;
                     for (i=0;i<6;i++){
                       var a = alpha[Math.floor(Math.random() * alpha.length)];
                       var b = alpha[Math.floor(Math.random() * alpha.length)];
                       var c = alpha[Math.floor(Math.random() * alpha.length)];
                       var d = alpha[Math.floor(Math.random() * alpha.length)];
                       var e = alpha[Math.floor(Math.random() * alpha.length)];
                       var f = alpha[Math.floor(Math.random() * alpha.length)];
                       var g = alpha[Math.floor(Math.random() * alpha.length)];
                      }
                     var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d + ' ' + e + ' ' + f + ' ' + g;
                     //-webkit-transform: rotate(-90deg);
                     var ran = 1;

                     var divCaptcha = document.getElementById("mainCaptcha");
                     var divHeight = divCaptcha.style.height;
                     var divWidth = divCaptcha.style.width;

                     var marginRight = 0, marginBottom = 0, marginleft = 0, marginTop = 0, transform = 0;

                     for (var i = 0; i < 20; i++) {
                         var span = document.createElement('span');

                         transform = 360 * Math.random() + i;

                         var chang = 60 * Math.random() + i;
                        // marginRight = divWidth - chang;//(marginRight + ran);
                        // marginBottom = divHeight - chang;// (marginBottom + ran);
                         marginleft = (divWidth - marginleft) / chang;// (marginleft + ran) + marginRight;
                         marginTop = (divHeight - marginTop) / chang;// (marginTop + ran) + marginBottom;

                         var styleString = //"left:" + (Math.random() + 1) * i + "px;" + 
                                           //"top:" + (Math.random() + 1) * i + "px;" +
                                           "margin-left:" + marginleft + "px;" +
                                           "margin-top:" + marginTop + "px;" +
                                           //"margin-right" + marginRight + "px;" +
                                           //"margin-bottom" + marginBottom + "px;" +
                                           //"padding:" + Math.floor(Math.random() + ran * i) + "px;" +
                                           "-webkit-transform:" + "rotate(" + transform + "deg);";

                         span.innerText = ".";
                         span.style = styleString;
                         document.getElementById("mainCaptcha").appendChild(span);
                         debugger
                         if (ran % 2 == 0) {
                             //ran = Math.ceil(Math.random() + ((ran + i) / 1));
                             ran = Math.ceil(Math.random() + ((ran + i) / 1));
                         }
                         else {
                             ran = Math.ceil(Math.random() + ((ran + i) / 2));
                         }
                     }
                     var span = document.createElement('p');
                     span.innerText = code;
                     document.getElementById("mainCaptcha").appendChild(span);
                     //document.getElementById("mainCaptcha").innerText += code

                  }
                  function ValidCaptcha(){
                      var string1 = removeSpaces(document.getElementById('mainCaptcha').innerText);
                      var string2 = removeSpaces(document.getElementById('txtInput').value);
                      if (string1 == string2){
                        return true;
                      }
                      else{        
                        return false;
                      }
                  }
                  function removeSpaces(string){
                    return string.split(' ').join('');
                  }
             </script>

    </form>
</body>
</html>
