$(function () {
    //开启一个定时器，设置一个适当的时间段，让图片实现不断切换效果，这里设置的是1000ms即是1m
    setInterval(function () {
        //先从隐藏域的flag中获取它的值
        var j = $("#flag").val();
        //如果这个值等于-1，这里约定鼠标正在该按钮上，什么都不做，直接返回。
        if (j == -1) {
            return;
        }
        //否则根据j的值，设置相应的事件
        var lits = $("#Nav li");
        if (j - 1 >= 0) {
            setStyleMouseLeave(lits[j - 1]);
        } else {
            setStyleMouseLeave(lits[3]);
        }
        setStyleMouseOver(lits[j]);
        j++;
        if (j > 3) {
            j = 0;
        }
        $("#flag").val(j);
    }, 3000);
    //获取div nav下所有的li元素，并为他们绑定鼠标进入，和鼠标离开事件
    $("#Nav li").mouseover(function (i) {
        //该函数修改自己的样式，以及图片
        setStyleMouseOver(this);
        //设置兄弟元素的样式，即是其余的两个li
        $(this).siblings().css({
            color: 'rgb(102, 102, 102)',
            fontWeight: 'normal',
        });
        //设置flag的值为-1，以达到暂停定时器的效果
        $("#flag").val("-1");
    })
       .mouseleave(function () {
           //当鼠标离开时，要设置自己样式
           setStyleMouseLeave(this);
           //获取当前的li中n的属性，并根据n的值，来判断当再次执行定时器时，哪一个li被选中
           var n = $(this).attr("n");
           if (n / 1 + 1 <= 3) {
               n = n * 1 + 1;
           } else {
               n = 0;
           }
           //鼠标离开后更新flag的值，使flag的值，正好等于下次将要遍历到的li的属性n的值，以达到重启定时器的效果，但定时器从未停止，也没有重启功能
           $("#flag").val(n);
       });;
});
//封装出的两个公共的函数
function setStyleMouseOver(li) {
    $(li).css({
        color: 'rgb(255, 255, 255)',
        fontWeight: 'bolder',
    });
    var url = $(li).attr("url");
    $("#ShowImg").css("background-image", "url(" + url + ")");
}
function setStyleMouseLeave(li) {
    $(li).css({
        color: 'rgb(125, 125, 125)',
        fontWeight: 'normal',
    });
}