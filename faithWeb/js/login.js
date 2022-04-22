
$(function(){
 //    鼠标移入移出改变边框
 $("#btn_login").mousemove(function(){
     $("#btn_login").css("border-bottom","4px solid #FF5C00")
 })
 $("#btn_login").mouseout(function(){
     $("#btn_login").css("border","0")
 })
 $("#right_btn").mousemove(function(){
     $("#right_btn").css("border-bottom","4px solid #FF5C00")
 })
 $("#right_btn").mouseout(function(){
  $("#right_btn").css("border","0")
 })
 //点击切换signin signup的div
 $("#btn_login").click(function(){
     var txt1=$("#userName").val();
     var txt2=$("#userpwd").val();
     if(txt1!=''||txt2!=''){
      return
     }
     //清除文本改变样式
     $("#reguserName").val("");
     $("#reguserpwd").val("");
     $("#regReUserPwd").val("");
     $(".txt_click2").css("margin-left","20px");
     $(".txt_click2").css("line-height","80px");
     $(".txt_click").css("margin-left","20px");
     $(".txt_click").css("line-height","80px");
     $(".txt_click3").css("margin-left","20px");
     $(".txt_click3").css("line-height","80px");
 //设置sign-in Div可见
 $("#login_d").css("display","block")
 $("#reg_d").css("display","none")

 });

            $("#right_btn").click(function(){
                var txt1=$("#reguserName").val();
                var txt2=$("#reguserpwd").val();
                var txt3=$("#regReUserPwd").val();
                if(txt1!=''||txt2!=''||txt3!=''){
                return
                }
                $("#userName").val("");
                $("#userpwd").val("");
                $(".txt_click2").css("margin-left","20px");
                $(".txt_click2").css("line-height","80px");
                $(".txt_click").css("margin-left","20px");
                $(".txt_click").css("line-height","80px");
            //设置sign-up Div可见
            $("#login_d").css("display","none")
            $("#reg_d").css("display","block")

            });
           //设置选中 
           // $('#cb_login').attr('checked', true);
           $("#login_btn").click(function () {
            //获取用户名密码
            var userName = $("#userName").val();
           var userPwd = $("#userpwd").val();
           var data = {
               "userName": userName,
               "password": userPwd
           };
            //验证部分
           if(userName==""||userPwd==""){
             alert("文本框不能为空！")
             return;
           }

          //判断同意协议单选框
           var isChecked = $('#cb_login').is(":checked");
           if (isChecked)  {
               // 同意协议提交
               $.ajax({
               url: "http://localhost:5001/api/Authorize",
               type: "post",
               dataType: "json",
               data: JSON.stringify(data),
               contentType: "application/json",
               success: function (result) {
                   console.log(result);
                   if(result.code==0){
                    alert(result.msg)   
                   }else{
                    localStorage.setItem("token",result.token); 
                    localStorage.setItem("userid",result.id); 
                    localStorage.setItem("userName",result.name); 
                    window.location.href="main.html";
                   }    
               
               }, error: function (error) {
                   console.log(error)
               }

           });
           }else{
               alert("没有同意协议");
           }

})
$("#login_btn2").click(function(){
   var userName = $("#reguserName").val();
   var userPwd = $("#reguserpwd").val();
   var rePwd=$("#regReUserPwd").val();   
   var data = {
       "userName": userName,
       "password": userPwd
   };
   //验证
    //验证部分
    if(userName==""||userPwd==""||rePwd==""){
     alert("文本框不能为空！")
     return;
   }else{
       //对比密码框是否一直
       if(userPwd!=rePwd){
       alert("密码不一致！");
       }else{
           $.ajax({
            url: "http://localhost:5001/api/Login/regInfo",
            type: "post",
            dataType: "json",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (result) {
                if(result.code==0){
                  alert(result.msg);
                }else{
                    alert("注册成功！去登录吧！")
                }
                
            }, error: function (error) {
                console.log(error)
            }

   });
       }
   }
 
});
        });
            
       

function userClick(index){
 if(index==1){
     
    $(".txt_click").css("margin-left","10px");
     $(".txt_click").css("height","22px");
     $(".txt_click").css("line-height","25px");
 }
 if(index==2){
     $(".txt_click2").css("margin-left","10px");
     $(".txt_click2").css("height","22px");
     $(".txt_click2").css("line-height","25px");
 }
 if(index==3){
     $(".txt_click").css("margin-left","10px");
     $(".txt_click").css("height","22px");
     $(".txt_click").css("line-height","25px");
 }
 if(index==4){
     $(".txt_click2").css("margin-left","10px");
     $(".txt_click2").css("height","22px");
     $(".txt_click2").css("line-height","25px");
 }
 if(index==5){
     $(".txt_click3").css("margin-left","10px");
     $(".txt_click3").css("height","22px");
     $(".txt_click3").css("line-height","25px");
 }

}
function userBlur(index){
 if(index==1){
     var _txt=$("#userName").val();
    if(_txt!=''){
        return
    }
     $(".txt_click").css("margin-left","20px");
     $(".txt_click").css("line-height","80px");
 }
 if(index==2){
     var _txt=$("#userpwd").val();
    if(_txt!=''){
        return
    }
     $(".txt_click2").css("margin-left","20px");
     $(".txt_click2").css("line-height","80px");
 }
 if(index==3){
     
     var _txt=$("#reguserName").val();
   
    if(_txt!=''){
        return
    }
     $(".txt_click").css("margin-left","20px");
     $(".txt_click").css("line-height","80px");
 }
 if(index==4){
     var _txt=$("#reguserpwd").val();
   if(_txt!=''){
   return
    }
     $(".txt_click2").css("margin-left","20px");
     $(".txt_click2").css("line-height","80px");
 }
 if(index==5){
     var _txt=$("#regReUserPwd").val();
    if(_txt!=''){
        return
    }
     $(".txt_click3").css("margin-left","20px");
     $(".txt_click3").css("line-height","80px");
 }
}
