using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Answer;
internal sealed class
    CustomerAnswerSurveyCommandHandler(
        IRepositoryBase<Survey, Guid> surveyRepositoryBase,
        ICurrentUserService currentUserService,
        IRepositoryBase<SurveyResponse, Guid> surveyResponseRepositoryBase,
        IRepositoryBase<SurveyAnswer, Guid> surveyAnswerRepositoryBase,
        IRepositoryBase<ClassificationRule, Guid> classificationRuleRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Answer.Commands.CustomerAnswerSurveyCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Answer.Commands.CustomerAnswerSurveyCommand request,
        CancellationToken cancellationToken)
    {
        var survey = await surveyRepositoryBase.FindByIdAsync(request.SurveyId, cancellationToken);
        if (survey == null)
        {
            return Result.Failure(new Error("404", "Survey not found."));
        }

        var surveyResponse = new SurveyResponse
        {
            Id = Guid.NewGuid(),
            SurveyId = survey.Id,
            CustomerId = currentUserService.UserId,
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
        var rules = await classificationRuleRepositoryBase.FindAll(x => x.SurveyId == survey.Id)
            .ToListAsync(cancellationToken);
        var scoreByLabel = new Dictionary<string, int>();
        foreach (var rule in surveyAnswers.Select(answer => rules.Where(x => x.OptionValue == answer.Answer))
                     .SelectMany(matchedRule => matchedRule))
        {
            if (rule.ClassificationLabel == null) continue;
            scoreByLabel.TryAdd(rule.ClassificationLabel, 0);

            scoreByLabel[rule.ClassificationLabel] += rule.Points;
        }

        //take key with max value in dictionary
        var maxScore = scoreByLabel.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        surveyResponseRepositoryBase.Add(surveyResponse);
        surveyAnswerRepositoryBase.AddRange(surveyAnswers);
        return Result.Success(maxScore);
    }
}