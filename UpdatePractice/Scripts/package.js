//데이터 출력
$(document).ready(function () {
    getAppList();
});

function ajaxPost(url, param, type, userState, fnSuccess, fnFail, async) {
    var posturl = _packUrl;
    posturl += "/" + url;

    if (!async) {
        async = false;
    }

    $.ajax({
        type: "POST",
        url: posturl,
        data: param,
        //timeout: 3000,
        dataType: "json",
        userType: type,
        userState: userState,
        async: async,
        success: function (data, status) {
            if (fnSuccess != null)
                fnSuccess(data, status, this.userType, this.userState);
        },
        error: function (xhr, status, error) {
            if (fnFail != null)
                fnFail(xhr, status, error, this.userType, this.userState);
        }
    });
}


//APP추가
function InsertNewApp() {
    var new_appName = $('#new_appName').val();
    var new_appCmd = $('#new_appCmd').val();
    var new_server = $('#new_server').val();
    var new_path = $('#new_path').val();
    var new_memo = $('#new_memo').val();
    var new_protocol = $('#new_protocol option:selected').val();

    var param = {
        appName: new_appName,
        appCmd: new_appCmd,
        server: new_server,
        path: new_path,
        memo: new_memo,
        protocol: new_protocol
    };

    ajaxPost("InsertNewApp", param, null, null, function (data) {
        if (data) {
            alert("앱 등록에 성공하였습니다.");
        } else {
            alert("앱 등록에 실패하였습니다.");
        }
    });
    getAppList();
}

//APP수정
function UpdateApp(item) {
    let tr = $(item).parent().parent();

    let appName = $(tr).find(".appName").val();
    let seq = $(tr).find(".seq").val();
    let appCmd = $(tr).find(".appCmd").val();
    let version = $(tr).find(".version").val();
    let server = $(tr).find(".server").val();
    let path = $(tr).find(".path").val();
    let memo = $(tr).find(".memo").val();
    let protocol = $(tr).find(".protocol option:selected").val();

    let param = {
        seq: seq,
        appName: appName,
        appCmd: appCmd,
        version: version,
        server: server,
        path: path,
        memo: memo,
        protocol: protocol
    }

    ajaxPost("UpdateApp", param, null, null, function (data) {
        if (data) {
            alert("저장되었습니다.");
        } else {
            alert("저장에 실패하였습니다.");
        }
    });
    getAppList();

}

//상태 변경
function UpdateState(item) {
    let tr = $(item).parent().parent();
    let state = $(tr).find(".state").text();
    if (state == '정상') {
        let seq = $(tr).find(".seq").val();
        let param = {
            seq: seq
        }
        ajaxPost("StopState", param, null, null, function (data) {
            if (data) {
                alert("업데이트를 중지합니다.");
            } else {
                alert("다시 시도하세요.");
            }
        });
    } else {
        let seq = $(tr).find(".seq").val();
        let param = {
            seq: seq
        }
        ajaxPost("UpdateState", param, null, null, function (data) {
            if (data) {
                alert("업데이트를 시작합니다.");
            } else {
                alert("다시 시도하세요.");
            }
        });
    };
    getAppList();
}

//패키지 삭제
function DeleteApp(item) {
    let tr = $(item).parent().parent();

    let seq = $(tr).find(".seq").val();
    let param = {
        seq: seq
    }
    ajaxPost("DeleteApp", param, null, null, function (data) {
        if (data) {
            alert("패키지가 삭제되었습니다.");
        } else {
            alert("다시 시도하세요.");
        }
    });
    getAppList();
}

//파일관리
function GoFilePage(item) {
    let tr = $(item).parent().parent();
    let seq = $(tr).find(".seq").val();

    location.href = "/File/Index?num=" + seq;

}
//xml
function GoXml(item) {
    let tr = $(item).parent().parent();
    let seq = $(tr).find(".seq").val();
    let appName = $(tr).find(".appName").val();
    let version = $(tr).find(".version").val();
    let protocol = $(tr).find(".protocol option:selected").val();
    let server = $(tr).find(".server").val();
    let path = $(tr).find(".path").val();
    let state = $(tr).find(".state").text();

    let param = {
        seq: seq,
        appName: appName,
        version: version,
        protocol: protocol,
        server: server,
        path: path,
        state: state
    }
    function ajaxPost(url, param, type, userState, fnSuccess, fnFail, async) {
        var posturl = _xmlUrl;
        posturl += "/" + url;

        if (!async) {
            async = false;
        }

        $.ajax({
            type: "POST",
            url: posturl,
            data: param,
            //timeout: 3000,
            dataType: "json",
            userType: type,
            userState: userState,
            async: async,
            success: function (data, status) {
                if (fnSuccess != null)
                    fnSuccess(data, status, this.userType, this.userState);
            },
            error: function (xhr, status, error) {
                if (fnFail != null)
                    fnFail(xhr, status, error, this.userType, this.userState);
            }
        });
    }

    ajaxPost("GetXml", param, null, null, function (data) {
        if (data) {
            
        } else {
            
        }
     });
   // window.open("Xml/Index?seq=" + seq);
}

//데이터 가져오기
function getAppList() {
    ajaxPost("GetAppList", null, null, null, function (data) {
        let noticeHtml = null;

        for (let i = 0; i < data.length; i++) {
            noticeHtml += '<tr><td><input type=type="text" class="textInput appName" value="' + data[i].appName + '"/></td>';
            noticeHtml += '<input type="hidden" class="seq" value="' + data[i].seq + '"/>';
            noticeHtml += '<td><input type=type="text" class="textInput appCmd" value="' + data[i].appCmd + '"/></td>';
            noticeHtml += '<td class="versionTd">';
            noticeHtml += '<input type="text" class="textInput version" id="version" value="' + data[i].version + '"/>';
            noticeHtml += '<input type="button" value="+" class="plus" /></td>';
            noticeHtml += ' <td class="serverTd"><select class="selec protocol">';
            if (data[i].protocol == 'http://') {
                noticeHtml += '<option value="http://" selected >http://</option>';
                noticeHtml += '<option value="https://">https://</option></select>';
            } else {
                noticeHtml += '<option value="http://" >http://</option>';
                noticeHtml += '<option value="https://" selected>https://</option></select>';
            }
            noticeHtml += '<input type="text" class="textInput server" id="serverInput" value="' + data[i].server + '" /></td>';
            noticeHtml += '<td><input type="text" class="textInput path" value="' + data[i].path + '"/></td>';
            noticeHtml += '<td><span class="state">' + data[i].state + '</span></td>';
            noticeHtml += '<td><textarea class="memo">' + data[i].memo + '</textarea></td>';
            noticeHtml += '<td class="btTd">';
            noticeHtml += '<input type="button" value="적용" class="bt" onclick="UpdateApp(this);"/>';
            if (data[i].state == '정상') {
                noticeHtml += '<input type="button" value="중지" class="bt stateBt" onclick="UpdateState(this);"/>';
            } else {
                noticeHtml += '<input type="button" value="정상" class="bt stateBt" onclick="UpdateState(this);"/>';
            }

            noticeHtml += '<input type="button" value="파일관리" class="bt" onclick="GoFilePage(this);"/>';
            noticeHtml += '<input type="button" value="삭제" class="bt" onclick="DeleteApp(this);"/>';
            noticeHtml += '<input type="button" value="XML" class="bt" onclick="GoXml(this)"/></td></tr>';

        }
        $('.app_list').html(noticeHtml);
    });
}








