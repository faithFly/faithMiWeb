function listClick(obj){
    //获取li下标
  var index=$(obj).index();
  //获取li 下a标签中的值
  var prodId=$(".samll_pro li:eq('"+index+"') a").text();  
  $(location).attr('href', '../view/details.html');
  //保存prodId到 localStorage
  localStorage.setItem("prodId",prodId);
  }
   function selectProdByName(prodName){
                 $("#list_con").empty();
                  //加载全部
                  var data={
                    "name": prodName,
                    "pageNum": 1,
                    "pageSize": 12
                  }
                  $.ajax({
                    url: "http://112.124.10.13:5001/api/listMain/GetProductPageNum",
                    type: "post",
                    dataType:"json",
                    data:JSON.stringify(data),
                    contentType: "application/json",
                    success: function (res) {
                      var html="";
                      for(let i of res.item1){
                          html +=` <li onclick="listClick(this)">
                            <a style="display:none;">${i.productId}</a>
                            <img src="../${i.productPicture}" alt="">
                            <h3>${i.productName}</h3>
                            <p>${i.productTitle}</p>
                            <span>${i.productPrice}元起</span>
                          </li>`
                        }
                        $('#list_con').html(html)
                     //调用分页组件
                      layui.use('laypage', function(){
                          var laypage = layui.laypage;
                          
                          //执行一个laypage实例
                          laypage.render({
                            elem: 'test1' //注意，这里的 test1 是 ID，不用加 # 号
                            ,count: res.item2, //数据总数，从服务端得到
                            limit:12,
                            limits:[10, 20, 30, 40, 50],
                            jump: function(obj, first){
                        //首次不执行
                          if(!first){
                            var data={
                                "name": prodName,
                                "pageNum": obj.curr,
                                "pageSize": obj.limit
                              }
                              $.ajax({
                                url: "http://112.124.10.13:5001/api/listMain/GetProductPageNum",
                                  type: "post",
                                  dataType:"json",
                                  data:JSON.stringify(data),
                                  contentType: "application/json",
                                  success: function (res) {
                                    //重置
                                    $("#list_con").empty();
                                    var html="";
                                    for(let i of res.item1){
                                        html +=` <li onclick="listClick(this)">
                                          <a style="display:none;">${i.productId}</a>
                                          <img src="../${i.productPicture}" alt="">
                                          <h3>${i.productName}</h3>
                                          <p>${i.productTitle}</p>
                                          <span>${i.productPrice}元起</span>
                                        </li>`
                                      }
                                      $('#list_con').html(html)
                                  },error:function(err){
                                    console.log(error)
                                  }
                              })
                          }
                        }
                          });
                        });
                    }, error: function (error) {
                        console.log(error)
                    }
                });
  }
   function selectAllProd(){
    $("#list_con").empty();
      //加载全部
      var data={
        "name": "全部",
        "pageNum": 1,
        "pageSize": 12
      }
      const aaa=(val)=>{
        console.log(val);
      }
      $.ajax({
        url: "http://112.124.10.13:5001/api/listMain/GetProductPageNum",
        type: "post",
        dataType:"json",
        data:JSON.stringify(data),
        contentType: "application/json",
        success: function (res) {
         //显示第一页
         var html="";
         for(let i of res.item1){
             html +=` <li onclick="listClick(this)">
              <a style="display:none;">${i.productId}</a>
              <img src="../${i.productPicture}" alt="">
              <h3>${i.productName}</h3>
              <p>${i.productTitle}</p>
              <span>${i.productPrice}元起</span>
            </li>`
           }
           $('#list_con').html(html)
           
         //调用分页组件
         layui.use('laypage', function(){
            var laypage = layui.laypage;
            
            //执行一个laypage实例
            laypage.render({
              elem: 'test1' //注意，这里的 test1 是 ID，不用加 # 号
              ,count: res.item2, //数据总数，从服务端得到
              limit:12,
              limits:[10, 20, 30, 40, 50],
              jump: function(obj, first){
           //首次不执行
            if(!first){
              var data={
                  "name": "全部",
                  "pageNum": obj.curr,
                  "pageSize": obj.limit
                }
                $.ajax({
                  url: "http://112.124.10.13:5001/api/listMain/GetProductPageNum",
                    type: "post",
                    dataType:"json",
                    data:JSON.stringify(data),
                    contentType: "application/json",
                    success: function (res) {
                      //重置
                      $("#list_con").empty();
                      var html="";
                      for(let i of res.item1){
                          html +=` <li onclick="listClick(this)">
                            <a style="display:none;">${i.productId}</a>
                            <img src="../${i.productPicture}" alt="">
                            <h3>${i.productName}</h3>
                            <p>${i.productTitle}</p>
                            <span>${i.productPrice}元起</span>
                          </li>`
                        }
                        $('#list_con').html(html)
                    },error:function(err){
                      console.log(error)
                    }
                })
            }
          }
             });
          });
      
      
        }, error: function (error) {
            console.log(error)
        }
    });
  }
    $(function(){
       // 获取token  
        var token = localStorage.getItem('token');
        var userName=localStorage.getItem('userName');
        //如果有token证明已经登录
        if(token!=""){
        //登录成功后实现的特效隐藏前两个a标签
        $("#topbar_changeA a").eq(0).css("display","none");
        $("#topbar_changeA a").eq(1).css("display","none");
        $("#topbar_changeA a").eq(2).html(userName);
        $("#topbar_changeA span").eq(0).css("display","none");

        }
            selectAllProd();
            //li点击事件
            $(".title_height li").click(function(){
                //获取li的下标
                var index=$(this).index();   
                //获取当前li的内容 
                var title=$(".title_height li:eq('"+index+"')").text();  
                if(title=="全部"){
                  selectAllProd();
                }else {
                selectProdByName(title);
                }
               
          });
    //点击购物车跳转
    $(".icon_txt").click(function(){
      var uid = localStorage.getItem('userid');
      if(uid==null||uid==""){
        layer.msg("还没有登录呢！"); 
      }
      window.location.href="shoppingCart.html";

    })
          
  })
