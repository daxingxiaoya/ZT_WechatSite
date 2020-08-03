var pageindex = 1;//页码
var pagesize = 10;//分页大小
var totalcoun = 0;//总数
var selUserId = 0;//选中的用户数据id

$(function () {
    LoadContet(1);//数据初始化

})


//数据初始化 
function LoadContet(index) {
    pageindex = index;
    $.ajax({
        type: "GET",
        url: '/WechatUsers/Getuserlist',
        dataType: "json", //表示返回值类型，不必须
        data: {
            page: index,
            pagesize: pagesize
        },
        success: function (jsondata) {
            if (jsondata.code == 0) {
                totalcoun = jsondata.count;//公告总数
                $("#tablecon").html(jsondata.tablehtml);
            }
        },
        complete: function () {
            if (totalcoun > 0) {
                //分页
                $("#pager").zPager({
                    totalData: totalcoun,
                    pageData: pagesize,
                    current: pageindex,
                    htmlBox: $('#wraper'),
                    btnShow: true,
                    ajaxSetData: false
                });
            } else {
                $("#pager").html("未查询到相关数据...");
            }
            RecClick();
        }
    });
}

//分页触发
function currentPage(currentPage) {
    if (pageindex != currentPage) {
        LoadContet(currentPage);
    }
}

//编辑按钮点击事件
function RecClick(){
    $("#btn_edit").click(function () {
        $(".default").addClass("hover_s");
        var ids = '';
        $('input[name="table_records"]:checked').each(function () {
            ids += $(this).val() + ',';
        });
        selUserId = ids.split(',')[0];
        console.log("UserId:", selUserId);
        if (selUserId != 0) {
            $('#tcmodal').modal('show');
            $.ajax({
                type: "GET",
                url: '/WechatUsers/GetUserInfoById',
                dataType: "json", //表示返回值类型，不必须
                data: {
                    userid: selUserId
                },
                async: false,
                success: function (jsondata) {
                    console.log("", jsondata);
                    if (jsondata.code == 0) {
                        $("#hiduid").val(jsondata.userinfo.unionid);
                        $("#username").val(jsondata.userinfo.nickname);//用户名
                        $("#companyname").val(jsondata.userinfo.companyname);//公司名
                        $("#check_ApMenu").html(jsondata.apmenu);//小程序菜单
                        $("#check_RA").html(jsondata.RA);//库区
                        $("#radio_status_" + jsondata.userinfo.empowerStatus).prop("checked", true);//审核状态
                    }
                },
                complete: function () {
                    ApMenucheck();//复选框点击事件
                    RAcheck();//复选框点击事件
                    btnsave();//编辑框保存点击事件
                }
            });
        } else {
            $('#tcmodal').modal('hide');
        }
    })

}
//小程序菜单复选框
function ApMenucheck() {
    //①全选框：全选和全不选
    $("#selAll_ApMenu").click(function () {
        if ($(this).prop("checked")) {
            $("input[name='scheck_ApMenu']:checkbox").prop("checked", true);
        } else {
            $("input[name='scheck_ApMenu']:checkbox").prop("checked", false)
        }
    });
    //②复选框：点满后将全选框选中，否则取消选中
    $("input[name='scheck_ApMenu']").click(function () {
        //获取被选中复选框的个数
        var checkedCount = $("input[name='scheck_ApMenu']:checked").length;
        //获取所有的复选框的个数
        var totalCount = $("input[name='scheck_ApMenu']").length;

        if(checkedCount == totalCount){//全选了
            $("#selAll_ApMenu").prop("checked" , true);
        }else{//没有全选
            $("#selAll_ApMenu").prop("checked" , false);
        } 
    });
};
//库区权限复选框
function RAcheck() {
    $("#selAll_RA").click(function () {
        if ($(this).prop("checked")) {
            $("input[name='scheck_RA']:checkbox").prop("checked", true);
        } else {
            $("input[name='scheck_RA']:checkbox").prop("checked", false)
        }
    });
    $("input[name='scheck_RA']").click(function () {
        var checkedCount = $("input[name='scheck_RA']:checked").length;
        var totalCount = $("input[name='scheck_RA']").length;
        if (checkedCount == totalCount) {//全选了
            $("#selAll_RA").prop("checked", true);
        } else {//没有全选
            $("#selAll_RA").prop("checked", false);
        } 
    });
};

//编辑框保存点击事件
function btnsave() {
    $("#btn_modalsave").click(function () {
        var ApMenus = '';//小程序菜单权限
        $('input[name="scheck_ApMenu"]:checked').each(function () {
            ApMenus += $(this).val() + ',';
        });
        var RAs = '';//库区权限
        $('input[name="scheck_RA"]:checked').each(function () {
            RAs += $(this).val() + ',';
        });
        $.ajax({
            type: "GET",
            url: '/WechatUsers/UpUserEmpower',
            dataType: "json", //表示返回值类型，不必须
            data: {
                uid: $("#hiduid").val(),
                ApMenus: ApMenus,
                RA: RAs,
                radioestatus: $('input[name="radioestatus"]:checked').val()
            },
            success: function (jsondata) {
                //修改成功后刷新数据
                if (jsondata.code == 0) {
                    window.location.reload();
                }
            },
            complete: function () {
                ////清空编辑框
                //clear();
            }
        });
    });
}