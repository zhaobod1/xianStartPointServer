function fc(val) {
    var c = val;
    if (c.length > 0) c = c.substring(0, c.length - 1);

    c = c.replace("ld", "路段");

    c = c.replace("jwd", "经纬度");

    c = c.replace("mtlx", "媒体类型");

    c = c.replace("mtxs", "媒体形式");

    c = c.replace("glzh", "实际桩号");

    c = c.replace("htzh", "合同桩号");

    c = c.replace("sxx", "上下行");

    c = c.replace("gg2", "规格2");
    c = c.replace("gg", "规格1");

    c = c.replace("lzxkh", "路政许可号");
    c = c.replace("jzjjl", "距桩界距离");

    c = c.replace("jcsj", "建成时间");

    c = c.replace("spsj", "审批时间");

    c = c.replace("qysj", "启用时间");

    c = c.replace("synx", "使用年限");

    c = c.replace("ckbj", "参考报价");

    c = c.replace("yz", "原值");

    c = c.replace("xgt", "效果图");

    c = c.replace("pmt", "平面图");

    c = c.replace("bfsj", "报废时间");

    c = c.replace("bfcz", "报废残值");

    c = c.replace("bfyy", "报废原因");

    c = c.replace("zygl", "媒体编号");

    c = c.replace("bz", "备注");

    c = c.replace("htlx", "合同类型");

    c = c.replace("hth", "合同号");

    c = c.replace("qsrq", "起始日期");

    c = c.replace("jsrq", "结束日期");

    c = c.replace("khmc", "客户名称");

    c = c.replace("fksj", "付款时间");

    c = c.replace("mtxx", "媒体信息");

    c = c.replace("htje", "合同金额");
   
    c = c.replace("ywy", "业务员");
    c = c.replace("skxx", "收款信息");
    c = c.replace("sjjm", "实际减免");
    c = c.replace("ysk", "已收款");
    c = c.replace("fj", "附件");
    c = c.replace("bz", "备注");
    c = c.replace("srlx", "收入类型");
    c = c.replace("skrq", "收款日期");
    c = c.replace("hth", "合同号");
    c = c.replace("jkdw", "缴款单位");
    c = c.replace("mtxx", "媒体信息");
    c = c.replace("skje", "收款金额");
    c = c.replace("jm", "减免");
    c = c.replace("kpsj", "开票时间");
    c = c.replace("kpje", "开票金额");
    c = c.replace("ysk", "已收款");
    c = c.replace("dqwsk", "到期未收款");   
    c = c.replace("jbr", "经办人");
    c = c.replace("zt", "状态");
    c = c.replace("bz", "备注");
    
    
    c = c.replace("khmc", "客户名称");

    c = c.replace("khdj", "客户等级");

    c = c.replace("dz", "地址");

    c = c.replace("yb", "邮编");

    c = c.replace("lxr", "联系人");

    c = c.replace("sj", "手机");

    c = c.replace("email", "Email");

    c = c.replace("dh", "电话");

    c = c.replace("fj", "附件");

    c = c.replace("khpj", "客户评价");

    c = c.replace("bz", "备注");
    return c;
}