class Component {
    constructor(param_id, param_name, param_innerHTML, param_value, param_tag) {
        this.id = param_id;
        this.name = param_name;
        this.innerHTML = param_innerHTML;
        this.value = param_value;
        this.tag = param_tag;
        this.element = document.createElement(param_tag);
        this.setAttr();
    }

    //setters
    setAttr() {
        this.element.id = this.id;
        this.element.name = this.name;
        this.element.innerHTML = this.innerHTML;
        this.element.value = this.value;
    }
    setResponderElementId(param_responderElementId) {
        this.responderElementId = param_responderElementId;
    }

    //getters
    getId() {
        return this.id;
    }
    getName() {
        return this.name;
    }
    getInnerHTML() {
        return document.getElementById(this.id).innerHTML;
    }
    getValue() {
        return document.getElementById(this.id).value;
    }
    getResponderElementId() {
        return this.responderElementId;
    }

    //functions
    getNodes() {
        return document.getElementById(this.id).children;
    }
    updateText(param_text) {
        this.updateInnerHTML(param_text);
    }
    updateInnerHTML(param_text, append) {
        if (append == true) {
            param_text = this.element.innerHTML + param_text;
        }
        document.getElementById(this.id).innerHTML = param_text;
        this.setInnerHTML(param_text);
        this.element = document.getElementById(this.id).innerHTML;
    }
    updateList(param_array) {
        var result = "<ul>";
        for (i = 0; i < param_array.length; i++) {
            result = result + "<li>" + param_array[i] + "</li>";
        }
        result = result + "</ul>";
        this.element.updateInnerHTML(result);
    }
    updateVale(param_value) {
        document.getElementById(this.id).value = param_value;
        this.setValue(param_value);
    }
    addToElement(param_elementId) {
        document.getElementById(param_elementId).innerHTML = this.element;
    }

    //ajax
    getRequest(controller, query, param_callBackFunctionName) {
        $.get(controller + "?" + query, Window[param_callBackFunctionName])
    }
    getRequest(controller, query) {
        $.get(controller + "?" + query, ajax_getCallBack)
    }
    ajax_getCallBack(data) {
        document.getElementById(this.responderElementId).style.visibility = "visible";
        document.getElementById(this.responderElementId).innerHTML = "";
        for (i = 0; i < data.length; i++) {
            var res = JSON.parse(data)[i];
            document.getElementById(this.responderElementId).innerHTML = document.getElementById(this.responderElementId).innerHTML + res + "<br />";
        }
    }

};