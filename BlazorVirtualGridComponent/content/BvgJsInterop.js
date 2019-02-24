function getTime() {
    var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());
    var s = addZero(d.getSeconds());
    var ms = addZero(d.getMilliseconds(), 2);
    return h + ":" + m + ":" + s + "." + ms;
}

function addZero(i, j = 1) {
    if (i < 10) {
        i = "0" + i;
    }
    if (j === 2) {
        if (i < 100) {
            i = "0" + i;
        }
    }
    return i;
}





window.BvgJsFunctions = {
    GetElementActualWidth: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.width;
        }
        else {
            return 0;
        }
    },
    GetElementActualHeight: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.height;
        }
        else {
            return 0;
        }
    },
    GetElementActualTop: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.y;
        }
        else {
            return 0;
        }
    },
    GetWindowHeight: function () {
        return window.innerHeight
            || document.documentElement.clientHeight
            || document.body.clientHeight;
       
    },
    SetElementScrollLeft: function (el, val) {
        if (document.getElementById(el) !== null) {
            document.getElementById(el).scrollLeft=val;
            return true;
        }
        else {
            return false;
        }
    },
    GetElementScrollLeft: function (el) {
        if (document.getElementById(el) !== null) {
            return document.getElementById(el).scrollLeft + document.getElementById(el).clientWidth;
        }
        else {
            return 0;
        }
    },
    SetFocus: function (el) {
        if (document.getElementById(el) !== null) {
            console.log("js set focus to " + el);
            document.getElementById(el).focus();
            return true;
        }
        else {
            console.log("js set focus not found " + el);
            return false;
        }
    },
    UpdateElementContentBatchMonoString: function (l) {
        console.warn("monostring");
        b = JSON.parse(Blazor.platform.toJavaScriptString(l));

        for (var i = 0; i < b.length; i += 3) {
            if (b[i + 2] === "b") {
              
                if (b[i + 1].toLowerCase() === "true") {
                    document.getElementById("checkbox" + b[i]).checked = true;
                }
                else {
                    document.getElementById("checkbox" + b[i]).checked = false;
                }
            }
            else {
                if (document.getElementById(b[i]) !== null) {

                    document.getElementById(b[i]).removeChild(document.getElementById(b[i]).lastChild);

                    var c = document.createTextNode(b[i + 1]);
                    document.getElementById(b[i]).appendChild(c);

                    //document.getElementById(b[i]).innerText = JSON.stringify(b[i + 1]);
                }
            }
        }

        
        return true;
    },
    UpdateElementContentBatchMonoByteArray: function (l) {

        b = JSON.parse(new TextDecoder("utf-8").decode(Blazor.platform.toUint8Array(l)));

        for (var i = 0; i < b.length; i += 2) {
            if (document.getElementById(b[i]) !== null) {
                document.getElementById(b[i]).innerText = b[i + 1];
            }
        }

        return true;
    },
    SetValueToCheckBox: function (el, val) {

        if (document.getElementById(el) !== null) {
            if (val.toLowerCase() === "true") {
                document.getElementById(el).checked = true;
            }
            else {
                document.getElementById(el).checked = false;
            }
        }


        return true;
    },
    
    
};
