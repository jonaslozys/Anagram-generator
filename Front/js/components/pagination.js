const Pagination = (currentPage) => {
    const previousDisabled = currentPage < 3;
    const pagination = 
    `
        <nav aria-label="...">
            <ul class="pagination">
                <li class="page-item ${previousDisabled ? "disabled" : ''}">
                    <button class="page-link" href="#" value=${currentPage - 1} tabindex="${currentPage - 1}">Previous</button>
                </li>
                <li class="page-item">
                    <button class="page-link"  ${previousDisabled ? 'disabled' : ''} value=${currentPage - 1} href="#">${currentPage - 1} </button>
                </li>
                <li class="page-item active">
                    <button class="page-link" value=${currentPage} href="#">${currentPage}<span class="sr-only">(current)</span></button>
                </li>
                <li class="page-item">
                    <button class="page-link" value=${currentPage + 1} href="#">${currentPage + 1}</button>
                </li>
                <li class="page-item">
                    <button class="page-link" value=${currentPage + 1} href="#">Next</button>
                </li>
            </ul>
        </nav>
    `;
    return pagination;
}

export default Pagination;