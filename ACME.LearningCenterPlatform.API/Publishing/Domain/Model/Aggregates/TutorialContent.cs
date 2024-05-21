using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial : IPublishable
{
    
    public ICollection<Asset> Assets { get; }
    
    public EPublishingStatus Status { get; protected set; }

    public bool Readable => HasReadableAssets;

    public bool Viewable => HasViewableAssets;

    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);

    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);

    private bool HasAllAssetsWithStatus(EPublishingStatus status) => 
        Assets.All(asset => asset.Status == status);
    
    
    public void SendToEdit()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToApproval))
            Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApproveAndLock()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ApprovedAndLocked))
            Status = EPublishingStatus.ApprovedAndLocked;
    }

    public void Reject() => Status = EPublishingStatus.Draft;

    public void ReturnToEdit() => Status = EPublishingStatus.ReadyToEdit;

    public List<ContentItem> GetContent()
    {
        var content = new List<ContentItem>();
        if (Assets.Any())
            content.AddRange(Assets.Select(asset => 
                new ContentItem(asset.Type.ToString(), asset.GetContent() as string 
                                                       ?? string.Empty)));
        return content;
    }

    private bool ExistsImageWithUrl(string imageUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Image &&
                                   (string)asset.GetContent() == imageUrl);
    }

    public void AddImage(string imageUrl)
    {
        if (ExistsImageWithUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }
    
    
    
}