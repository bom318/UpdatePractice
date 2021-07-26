//데이터 출력
$(document).ready(function () {
    getFileList();
});

function ajaxPost(url, param, type, userState, fnSuccess, fnFail, async) {
    var posturl = _fileUrl;
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

function getFileList() {
    let seq = window.location.search.substring(5)
    let param = {
        seq: seq
    }
    ajaxPost("GetFileList", param, null, null, function (data) {
        let noticeHtml = null;

        for (let i = 0; i < data.length; i++) {
            noticeHtml += '<tr><td><span class="seq">' + data[i].seq + '</span></td>';
            noticeHtml += '<td><span class="name">' + data[i].name + '</span></td>';
            noticeHtml += '<td><input type="text" class="textInput local" value="' + data[i].local + '" /></td>';
            noticeHtml += '<td><input type="text" class="textInput tagName" value="' + data[i].tagName + '" /></td>';
            noticeHtml += '<td><input type="text" class="version" value="' + data[i].version + '"/>';
            noticeHtml += '<input type="button" value="+" class="plus" onclick="versionUp(this);"/></td>';
            noticeHtml += '<td><input type="text" class="textInput type" value="' + data[i].type + '"/></td>';
            noticeHtml += '<td><input type="text" class="textInput reg" value="' + data[i].reg + '"/></td>';
            noticeHtml += '<td><span>' + data[i].size + '</span></td>';
            noticeHtml += '<input type="hidden" value="' + data[i].path + '" class="path" />';
            noticeHtml += '<td class="btTd"><input type="button" value="적용" class="bt" onclick="updateFile(this);"/>';
            noticeHtml += '<input type="button" value="삭제" class="bt" onclick="deleteFile(this);"/></td></tr>';
        }
        $('.file_list').html(noticeHtml);
    })
}
//버전 +
function versionUp(item) {
    let tr = $(item).parent().parent();
    let version = parseInt($(tr).find(".version").val());

    alert('aa')
    //$(tr).find(".version").val(version + 1);

}
//파일 업로드
function addFile() {
    if ($('#addInput').val() == "") {
        alert("파일이 선택되지 않았습니다")
    } else {
        let form = $('.addFile')[0];
        let formData = new FormData(form);
        formData.append("addFile", $('#addInput')[0].files[0]);
        formData.append("seq", window.location.search.substring(5));
        formData.append("local", $('#addInput').val());
       

        $.ajax({
            url: 'InsertNewFile',
            data: formData,
            type: 'POST',
            processData: false,
            contentType: false,
            success: function (result) {
                alert("파일 등록에 성공하였습니다.");
            },
            error: function (result) {
                alert("파일 등록에 실패하였습니다.");
            }
        });
        getFileList();
    }
}
//파일 업데이트1
function udtFile() {
    if ($('#udtInput').val() == "") {
        alert("파일이 선택되지 않았습니다")
    } else {
        let form = $('.updateFile')[0];
        let formData = new FormData(form);
        formData.append("updateFile", $('#udtInput')[0].files[0]);
        formData.append("seq", window.location.search.substring(5));
        formData.append("local", $('#udtInput').val());
        formData.append("reg", $('.reg').val());


        $.ajax({
            url: 'UpdateNewFile',
            data: formData,
            type: 'POST',
            processData: false,
            contentType: false,
            success: function (result) {
                alert("파일 업데이트에 성공하였습니다.");
            },
            error: function (result) {
                alert("파일 업데이트에 실패하였습니다.");
            }
        });
        getFileList();

    }
}

//파일 삭제
function deleteFile(item) {
    let tr = $(item).parent().parent();

    let seq = $(tr).find(".seq").text();
    let appSeq = window.location.search.substring(5);
    let name = $(tr).find(".name").text();
    let param = {
        seq: seq,
        appSeq: appSeq,
        name: name
    }

    ajaxPost("DeleteFile", param, null, null, function (data) {
        if (data) {
            alert("파일이 삭제되었습니다.");
        } else {
            alert("다시 시도하세요.");
        }
    });
    getFileList();
}

//파일 수정
function updateFile(item) {
    let tr = $(item).parent().parent();

    let name = $(tr).find(".name").text();
    let local = $(tr).find(".local").val();
    let tagName = $(tr).find(".tagName").val();
    let version = $(tr).find(".version").val();
    let type = $(tr).find(".type").val();
    let reg = $(tr).find(".reg").val();
    let path = $(tr).find(".path").val();
    let seq = window.location.search.substring(5);

    let param = {
        name: name,
        local: local,
        tagName: tagName,
        version: version,
        type: type,
        reg: reg,
        path: path,
        seq: seq
    }

    ajaxPost("UpdateFile", param, null, null, function (data) {
        if (data) {
            alert("저장되었습니다.");
        } else {
            alert("저장에 실패하였습니다.");
        }
    });
    getFileList();

}