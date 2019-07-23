class Renderer {
    constructor() {
        this.pageContent = "";
        this.el = document.getElementById("target");
    }

    render() {
        this.el.innerHTML = this.pageContent;
    }

    changePage(page) {
        this.pageContent = page;
        this.render();
    }
}

export default Renderer;