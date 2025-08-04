/*
    Spike test is a variation of a stress test, but it does not gradually increase the load,
    instead it spikes to extream load over a very short window of time

    Run a spike test to:
        - Determine how your system will perform under a sudden surge of traffic
        - Determine if your system will recover once the traffic has subsided

    Success is based on expectations. System will generally react in 1 or 4 ways
    - Excellent: system performance is not degraded during the surge of traffic.
                 Response time is similar during low traffic and high traffic
    - Good: Response time is slower, but the system does not produce and errors.
            All requests are handled
    - Poor: System produces error during the surge of traffic, but recovers to normal after the traffic subsuded
    - Bad: System crasshes, and does not recover after the traffic has subsuded
*/




import http from 'k6/http'

export let options = {
    insecureSkipTLSVerify: true,
    vus: 1,
    duration: '10s'
};

export default () => {
    http.get('https://localhost:7005/api/Books');
};