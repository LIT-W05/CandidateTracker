using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace CandidateTracker.Data
{
    public class CandidateRepository
    {
        private string _connectionString;

        public CandidateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Candidate> GetCandidates(Status status)
        {
            using (var context = new CandidateTrackerDataContext(_connectionString))
            {
                return context.Candidates.Where(c => c.Status == status).ToList();
            }
        }

        public void AddCandidate(Candidate candidate)
        {
            using (var context = new CandidateTrackerDataContext(_connectionString))
            {
                context.Candidates.InsertOnSubmit(candidate);
                context.SubmitChanges();
            }
        }

        public void UpdateStatus(int candidateId, Status status)
        {
            using (var context = new CandidateTrackerDataContext(_connectionString))
            {
                context.ExecuteCommand("UPDATE Candidates SET Status = {0} WHERE Id = {1}", status, candidateId);
            }
        }

        public CandidateCounts GetCounts()
        {
            using (var context = new CandidateTrackerDataContext(_connectionString))
            {
                var result = context.GetCandidateCounts();
                GetCandidateCountsResult counts = result.First();
                return new CandidateCounts
                {
                    Confirmed = counts.Confirmed.Value,
                    Pending = counts.Pending.Value,
                    Refused = counts.Refused.Value
                };
                //return new CandidateCounts
                //{
                //    Confirmed = context.Candidates.Count(c => c.Status == Status.Confirmed),
                //    Pending = context.Candidates.Count(c => c.Status == Status.Pending),
                //    Refused = context.Candidates.Count(c => c.Status == Status.Refused),
                //};

            }
        }

        public Candidate GetCandidate(int id)
        {
            using (var context = new CandidateTrackerDataContext(_connectionString))
            {
                return context.Candidates.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}