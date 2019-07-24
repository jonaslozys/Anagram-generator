const ErrorAlert = (errorMessage) => {
    const errorAlert = 
    `
        <div class="alert alert-danger" role="alert">
            ${errorMessage}
        </div>
    `

    return errorAlert;
}

export default ErrorAlert;