namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Answer;
internal sealed class DetermineSurveyAnswerCommandHandler(
    IRepositoryBase<ClassificationRule, Guid> classificationRuleRepositoryBase,
    IRepositoryBase<SurveyQuestion, Guid> surveyQuestionRepositoryBase,
    IRepositoryBase<SurveyAnswer, Guid> surveyAnswerRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Answer.Commands.DetermineSurveyAnswerCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Answer.Commands.DetermineSurveyAnswerCommand request,
        CancellationToken cancellationToken)
    {
        var rules = await classificationRuleRepositoryBase.FindAll(x => x.SurveyId == request.SurveyId)
            .ToListAsync(cancellationToken);
        var scoreByLabel = new Dictionary<string, int>();
        var surveyResponse = new SurveyResponse
        {
            Id = Guid.NewGuid(),
            SurveyId = request.SurveyId,
            CustomerId = Guid.NewGuid()
        };
        var surveyAnswers = request.SurveyAnswers
            .Select(x => new SurveyAnswer
            {
                Id = Guid.NewGuid(),
                Answer = x.Answer,
                SurveyQuestionId = x.SurveyQuestionId,
                SurveyResponseId = surveyResponse.Id
            })
            .ToList();
        foreach (var rule in surveyAnswers.Select(answer => rules.Where(x => x.OptionValue == answer.Answer))
                     .SelectMany(matchedRule => matchedRule))
        {
            if (rule.ClassificationLabel == null) continue;
            scoreByLabel.TryAdd(rule.ClassificationLabel, 0);

            scoreByLabel[rule.ClassificationLabel] += rule.Points;
        }

        //take key with max value in dictionary
        var maxScore = scoreByLabel.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        return Result.Success(maxScore);
    }
}