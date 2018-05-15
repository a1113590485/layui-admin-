var Grid = {
    config:{
       token:''
    },
    createNew: function (option) {
        var ORDERDATA = []  //排序字段{column:'',asc:'asc/desc'}
            , grid = {
                elem: option.elem,  //目标
                data: option.data,  //数据
                url: option.url,   // url
                singleSelect: option.singleSelect || false, //是否单选
                page: option.page || 1,   //页码
                pageSize: option.pageSize,  //每页显示行数
                pageGroup: option.pageGroup == undefined ? 5 : option.pageGroup,  //页码显示数量
                pageData: option.data,  //当前页数据
                searchData: option.searchData || {}, //查询条件
                align: option.align || 'left',  //对齐方式
                columns: option.columns  // 字段

                , loading: function () {

                }
                , closeLoading: function () {

                },

            }
        
        //获取数据
        function fnData(fn) {
            grid.loading();
            if (grid.data) {
                var DATA = $.extend(true, {}, grid.data);

                if (ORDERDATA.length > 0) {
                    DATA.rows.sort(function (a, b) {
                        var r = 0;
                        for (var i = ORDERDATA.length; I > 0; i--) {
                            var x = eval('a.' + ORDERDATA[i - 1].column),
                                y = eval('b.' + ORDERDATA[i - 1].column);
                            if (x == y) r = 0;
                            else {
                                r = (x > y ? 1 : -1) * (ORDERDATA[i - 1].asc == 'asc' ? 1 : -1);
                                break;
                            }
                        }
                        return r;
                    });
                }
                if (grid.pageSize) {
                    grid.total = grid.data.rows.length;
                    grid.pageData = DATA;
                    grid.pageData.rows = DATA.rows.slice(grid.pageSize * (grid.page - 1), grid.pageSize * grid.page);
                    /*获取数据后的事件*/
                    /*....*/
                    fn();
                }
            } else if (grid.url) {
                var url = grid.url;
                if (grid.pageSize) {
                    $.extend(grid.searchData, {
                        page: grid.page,
                        pageSize: grid.pageSize
                    });
                }
                if (ORDERDATA.length > 0) {
                    var orderStr = '';
                    $.each(ORDERDATA, function (_, order) {
                        orderStr += order.column + ' ' + order.asc + ',';
                    });
                    orderStr = orderStr.substr(0, orderStr.length - 1);
                    grid.searchData.orderString = orderStr;
                }
                $.ajax({
                    url: url,
                    data: grid.searchData,
                    type: 'get',
                    dataType: 'json',
                    beforeSend: function (xhr) {
                        if (config.token) {
                            xhr.setRequestHeader('Authorization', 'Basic ' + config.token);
                        }
                    },
                    success: function (data) {
                        $.extend(grid, {
                            pageData: data,
                            flag: data.flag,
                            total: data.total
                        });
                        /*获取数据成功后的事件*/
                        /*...*/
                        fn();
                    },
                    error: function (error) {
                        grid.flag = false;
                        fn();
                    }
                });
            }
            else {
                grid.pageData = { rows: [] };
                fn();
            }
        }

        var HTML = {
            header: function () {
                var html = $('<table><thead></thead></table>');
                $.each(grid.columns, function (i, column) {

                });
            }
        }
       

        return grid;
    }
}


page.grid = grid.createNew({
    url: 'api/menu',
    align: 'left',
    singleSelect: false,
    pageSize:20,
    rowClick: function (row) {

    },
    columnGroup: [[
        '',
        '编号',
        {title:'用户信息',colspan:4},
        {title:'账单信息',colspan:6}
    ], [
    ]],

    columns: [
        {
            field:'code',ckeckbox:true,align:'center'
        },
        {
            title: '编号', field: 'code', align: 'left', calss: ''
        },
        {
            title: '名称', field: 'title', align: 'center'
        },
        {
            title: '状态', field: 'status', align: 'center', formart: function (row) {
                return row.status == 0 ? '禁用' : '启用';
            }
        },
        {
            title: '执行数量', field: 'zxsl', edit: 'text', verify: 'required|number', min: '0', max: function (row) { return row.sl },
            formart: function (row) {
                return row.sl + row.dw;
            },
            editFormat: function () {
                return '<input type="text" min="0" max="' + row.sl + '" verify="required|number" />';
            }
        },
        {
            title: '操作', align: 'center', format: function (row, index) {
                return [
                    $('<a>删除</a>').click(function () { grid.delete(index) }),
                    $('<a>修改</a>').click(function () { grid.delete(index) })
                ]
            }
        }
    ],
    dataSuccess: function () {

    },
    success: function () {

    }
});

grid.url = '';
grid.bind();

//清除数据
grid.clear();

//删除数据
grid.delete(1); grid.delete([0, 1, 2, 3]);

//添加数据
grid.insert({ object}), grid.insert([array]), grid.insert({ object}, index), grid.insert([array], index);

//修改数据
grid.update({ object}), grid.update({object}, index);

//编辑数据
grid.edit(), grid.edit(index), grid.edit([array]), grid.edit('all');

//结束编辑
grid.endEdit(), grid.endEdit(index), grid.endEdit([array]);

//自适应窗口
grid.resize();