declare var tinymce: any;
declare var html2canvas: any;

const selectTemplate = (e: MouseEvent) => {
    const targetElement: any = e.target;
    const templateSrc = targetElement.src;

    const generatorMsg: HTMLElement = document.getElementById("post_generator-msg");
    generatorMsg.style.display = "none";

    const imageWrapper: HTMLElement = document.getElementById("post_generator-image_wrapper");
    imageWrapper.style.display = "block";
    imageWrapper.style.backgroundImage = `url(${templateSrc})`;
};

const selectFilter = (e) => {
    const selectedValue: any = e.target.value;

    const imageWrapper: HTMLElement = document.getElementById("post_generator-image_wrapper");
    imageWrapper.className = `image_wrapper-${selectedValue}`;
}

const writeText = (text) => {
    const contentElement: HTMLElement = document.getElementById("post_generator-content");
    contentElement.innerHTML = text;
}

const saveImage = (canvas) => {
    const dataURL = canvas.toDataURL("image/png", 1.0);

    downloadImage(dataURL, 'new post.png');
}

function downloadImage(data, filename = 'untitled.jpeg') {
    var a = document.createElement('a');
    a.href = data;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
}

const elements = document.getElementsByClassName("template_wrapper");

Array.from(elements).forEach(function (element) {
    element.addEventListener('click', selectTemplate);
});

document.getElementById('post_generator-select-filter').addEventListener('change', selectFilter);

document.getElementById('post_generator-save').addEventListener('click', () => {
    html2canvas(document.querySelector("#post_generator-image_wrapper"), {
        dpi: 166,
        onrendered: function (canvas) {
            saveImage(canvas);
        }
    })
});

tinymce.init({
    selector: 'textarea#post-generator-text',
    toolbar: "sizeselect | bold italic | fontselect |  fontsizeselect | align",
    toolbar_mode: 'floating',
    theme_advanced_font_sizes: "10px,12px,14px,16px,24px",
    setup: function (ed) {
        ed.on("click", function () {
            const editableContent = tinymce.get('post-generator-text').getContent();
            writeText(editableContent);
        });
        ed.on("keyup", function () {
            const editableContent = tinymce.get('post-generator-text').getContent();
            writeText(editableContent);
        });
        ed.on("NodeChange", function () {
            console.log(')))000')
            const editableContent = tinymce.get('post-generator-text').getContent();
            writeText(editableContent);
        });
    },
    height: 300,
});
