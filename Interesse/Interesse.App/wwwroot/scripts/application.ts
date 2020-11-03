interface ApplicationModel {
    name: string;
    phone: string;
    comment: string;
    type: string;
}

const showError = (msg: string) => {
    document.getElementById("app-success").innerHTML = "";
    document.getElementById("app-error").innerHTML = msg;
};

const showSuccess = (msg: string) => {
    document.getElementById("app-error").innerHTML = "";
    document.getElementById("app-success").innerHTML = msg;
};

const sendApplication = () => {
    const nameInput: HTMLInputElement = document.getElementById('app-name') as HTMLInputElement;
    const phoneInput: HTMLInputElement = document.getElementById('app-phone') as HTMLInputElement;
    const commentInput: HTMLInputElement = document.getElementById('app-comment') as HTMLInputElement;
    const typeElement: HTMLElement = document.getElementById('app-type') as HTMLElement;

    const name = nameInput.value;
    const phone = phoneInput.value;
    const comment = commentInput.value;
    const type = typeElement.dataset.type;

    if (!name || !phone) {
        showError("Заполните форму");
        return;
    }

    const phoneNumber = phone.replace(/\D/g, '');

    const data: ApplicationModel = {
        name,
        phone: phoneNumber,
        comment: comment,
        type,
    };

    fetch('/api/application', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    }).then(response => {
        if (response.status === 204) {
            showSuccess("Ваша заявка принята! Скоро мы вам позвоним")
            document.getElementById("app-form").style.display = "none";
            document.getElementById("app-actions").style.display = "none";
            setTimeout(() => {
                document.getElementById("app-close").click();
            }, 3000);
        } else {
            showError("Ошибка данных")
        }
    }).catch((error) => {
        console.error('Error:', error);
    });
};

document.getElementById('send-app').addEventListener('click', () => {
    sendApplication();
});


