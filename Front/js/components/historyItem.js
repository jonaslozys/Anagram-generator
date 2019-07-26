const HistoryItem = (historyLog) => 
`
    <li class="list-group-item" id=${historyLog.searchId}>
       <h4>User IP: <b>${historyLog.userIP}</b></h4>
       <h4>Word Searched: <b>${historyLog.wordSearched}</b></h4>
       <h4>Search date: <b>${historyLog.searchDate}</b></h4>
       <h4>Anagrams: <b>${historyLog.anagrams.map(anagram => anagram)}</b></h4>
    </li>
`;

export default HistoryItem;